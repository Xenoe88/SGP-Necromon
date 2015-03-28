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
    private float m_facingDirection; //-1 == left, 1 == right
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

    private Animator m_animator;

    public AudioSource m_audioSource;
    public AudioClip m_walkingSFX, m_swingSFX, m_jumpSFX, m_doubleJumpSFX;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_audioSource = GetComponent<AudioSource>();

        m_facingDirection = 1;
    }

    // Update is called once per frame
    void Update()
    {

        if (m_player.GetComponent<Entity>().m_health <= 0)
        {
            m_animator.SetInteger("PlayerAnim", 5);
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

        if (Input.GetKeyDown("l") && m_player.GetComponent<PlayerInventory>().m_selectedRune != null)
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
            m_player.transform.localScale = new Vector3(m_movement, 1);

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
            m_animator.SetInteger("PlayerAnim", 4);
        else if (m_isGrounded && m_animator.GetInteger("PlayerAnim") == 4)
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
        m_animator.SetInteger("PlayerAnim", 2);
        m_player.GetComponent<PlayerInventory>().Summon(m_player.transform.position + new Vector3(1 * m_facingDirection, 0, 0));
        print("pooi");
        
        //GameObject temp = (GameObject)Instantiate(m_player.GetComponent<PlayerInventory>().m_selectedRune,m_player.transform.position + new Vector3(1*m_facingDirection,0), Camera.main.transform.rotation);
        //temp.SendMessage("MakeNecro", SendMessageOptions.DontRequireReceiver);
    }

    void Bomb()
    {
        if (m_player.GetComponent<PlayerInventory>().GetNumBomb() > 0)
        {
            Instantiate(m_player.GetComponent<PlayerInventory>().m_bomb, m_player.transform.position, transform.rotation);
            m_player.GetComponent<PlayerInventory>().UseBomb();
        }
    }

    //following functions called in animation
    void Attack()
    {
        GameObject temp = (GameObject)Instantiate(m_hitBox, new Vector3(m_player.transform.position.x + (m_player.transform.localScale.x * 0.2f), m_player.transform.position.y, m_player.transform.position.z), transform.rotation);
        Physics2D.IgnoreCollision(temp.collider2D, m_player.collider2D);
        temp.SendMessage("SetPlayer", gameObject, SendMessageOptions.DontRequireReceiver);
        Destroy(temp, 0.2f);
    }

    void SwingSound() { AudioSource.PlayClipAtPoint(m_swingSFX, Camera.main.transform.position); }
}
