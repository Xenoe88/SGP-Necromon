using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    //ET
    //start 2:30 pm
    //pause 3:45 pm
    //start 5:25 pm
    //stop 8:55 pm
    //start 2:15 pm
    //stop 2:40 pm //on hold until player inventory is complete

    public float m_movement;
    public float m_facingDirection; //-1 == left, 1 == right
    public int m_jumpHeight;
    public Transform m_groundCheck;
    public LayerMask m_whatIsGround;
    public float m_groundRadius;
    public bool m_isGrounded;
    public bool canDoubleJump;
    public int m_fallRate;
    public GameObject m_jumpParticles;
    public int m_numActiveNecromon;
    public GameObject m_hitBox;
    public GameObject m_player;

    private Color m_startColor, m_endColor;
    public Vector3 m_RevivePositon = Vector3.zero, m_reLoadPosition = Vector3.zero;
    private bool m_inMenu = false;
    public int m_lastLevel;
    private float m_deathTimer = 0.0f;

    private Animator m_animator;

    public AudioSource m_audioSource;
    public AudioClip m_walkingSFX, m_swingSFX, m_jumpSFX, m_doubleJumpSFX;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_audioSource = GetComponent<AudioSource>();

        m_startColor = GetComponent<SpriteRenderer>().color;
        m_endColor = new Color(m_startColor.r, m_startColor.g, m_startColor.b, 0.0f);

        m_facingDirection = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_inMenu)
            return;

        //if (m_player.GetComponent<Entity>().m_health <= 0)
        if (m_player.GetComponent<Entity>().m_isAlive == false)
        {
            //m_animator.SetInteger("PlayerAnim", 0);

            //fade away
            if (renderer.material.color.a >= 0.0f)
            {
                m_deathTimer += Time.deltaTime;

                renderer.material.color = Color.Lerp(m_startColor, m_endColor, m_deathTimer * 0.5f);
            }

            //revive if we can
            if (renderer.material.color.a <= 0.0f && m_player.GetComponent<PlayerInventory>().GetNumRevives() > 0)
            {
                Revive();
            }


            return;
        }

        // left/right
        Move();

        Jump();

        //attack (function called in animation)
        if (Input.GetKeyDown("k"))
        {
            m_animator.SetInteger("PlayerAnim", 3);
        }

        if (Input.GetKeyDown("l") && m_player.GetComponent<PlayerInventory>().m_selectedRune != -1)
        {
            Summon();
        }

        if (Input.GetKeyDown("j"))
        {
            Bomb();
        }


    }

    void Move()
    {
        //dont move if stunned
        if (m_player.GetComponent<Entity>().m_status == Status.STUN)
            return;

        // get direction
        m_movement = Input.GetAxisRaw("Horizontal");

        //reverse controls if confused
        if (m_player.GetComponent<Entity>().m_status == Status.CONFUSE)
            m_movement *= -1f;

        //get facing direction
        if (m_movement == -1 || m_movement == 1)
            m_facingDirection = m_movement;

        // turn
        if (m_movement != 0)
        {
            m_animator.SetInteger("PlayerAnim", 1);
            m_player.transform.localScale = new Vector3(m_movement * -1.0f, 1);

            if (m_audioSource.isPlaying == false)
                m_audioSource.Play();
        }
        else
            m_animator.SetInteger("PlayerAnim", 0);

        // move
        m_player.rigidbody2D.velocity = new Vector2(m_movement * m_player.GetComponent<Entity>().m_speed * m_player.GetComponent<Entity>().m_statusModifier, m_player.rigidbody2D.velocity.y);



    }

    void Jump()
    {
        m_isGrounded = Physics2D.OverlapCircle(m_groundCheck.position, m_groundRadius, m_whatIsGround);

        if (!m_isGrounded)
            m_animator.SetInteger("PlayerAnim", 2);
        else if (m_isGrounded && m_animator.GetInteger("PlayerAnim") == 2)
            m_animator.SetInteger("PlayerAnim", 0);

        if (Input.GetKeyDown("w") && m_isGrounded)
        {
            AudioSource.PlayClipAtPoint(m_jumpSFX, Camera.main.transform.position);
            canDoubleJump = true;
            m_player.rigidbody2D.velocity = new Vector2(m_player.rigidbody2D.velocity.x, m_player.rigidbody2D.velocity.y + m_jumpHeight);

        }
        else if (Input.GetKeyDown("w") && canDoubleJump)
        {
            AudioSource.PlayClipAtPoint(m_doubleJumpSFX, Camera.main.transform.position);
            canDoubleJump = false;
            m_player.rigidbody2D.velocity = Vector2.zero;
            m_player.rigidbody2D.velocity = new Vector2(m_player.rigidbody2D.velocity.x, m_player.rigidbody2D.velocity.y + m_jumpHeight);
        }

    }

    void Summon()
    {
        m_animator.SetInteger("PlayerAnim", 4);
        m_player.GetComponent<PlayerInventory>().Summon(m_player.transform.position + new Vector3(1 * m_facingDirection, 0, 0));
        //print("pooi");

        //GameObject temp = (GameObject)Instantiate(m_player.GetComponent<PlayerInventory>().m_selectedRune,m_player.transform.position + new Vector3(1*m_facingDirection,0), Camera.main.transform.rotation);
        //temp.SendMessage("MakeNecro", SendMessageOptions.DontRequireReceiver);
    }

    void Bomb()
    {
        if (m_player.GetComponent<PlayerInventory>().GetNumBomb() > 0)
        {
            GameObject temp = (GameObject)Instantiate(m_player.GetComponent<PlayerInventory>().m_bomb, new Vector3(m_player.transform.position.x - (m_player.transform.localScale.x * 0.4f), m_player.transform.position.y + 0.8f, m_player.transform.position.z), transform.rotation);
            temp.rigidbody2D.AddForce(new Vector2(100 * (m_player.transform.localScale.x * -1), 50));
            m_player.GetComponent<PlayerInventory>().UseBomb();
        }
    }

    //following functions called in animation
    void Attack()
    {
        GameObject temp = (GameObject)Instantiate(m_hitBox, new Vector3(m_player.transform.position.x - (m_player.transform.localScale.x * 0.4f), m_player.transform.position.y, m_player.transform.position.z), transform.rotation);
        Physics2D.IgnoreCollision(temp.collider2D, m_player.collider2D);
        temp.SendMessage("SetPlayer", this.gameObject, SendMessageOptions.DontRequireReceiver);
        Destroy(temp, 0.2f);
    }

    void SwingSound() { AudioSource.PlayClipAtPoint(m_swingSFX, Camera.main.transform.position); }

    void ChangeScene() { Application.LoadLevel(6); }

    public void SetRevivePosition(Vector3 _pos) { m_RevivePositon = _pos; }

    public void Revive()
    {
        gameObject.transform.position = m_RevivePositon;
        renderer.material.color = m_startColor;
        m_player.GetComponent<PlayerInventory>().UseRevives();
        m_player.GetComponent<Entity>().m_health = 200;
        m_player.GetComponent<Entity>().m_isAlive = true;
    }

    public void EnterExitMenu() 
    {
        print("BEFORE: " + m_inMenu);
        //m_inMenu = !m_inMenu;
        print("AFTER: " + m_inMenu);

    }
}
