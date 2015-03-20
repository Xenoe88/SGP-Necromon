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

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
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

        if (Input.GetKeyDown("k"))
        {
            m_animator.SetInteger("PlayerAnim", 3);
        }
            //Attack();

        if (Input.GetKeyDown("l"))
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
        // get direction
        m_movement = Input.GetAxisRaw("Horizontal");

        // turn
        if (m_movement != 0)
        {
            m_animator.SetInteger("PlayerAnim", 1);
            m_player.transform.localScale = new Vector3(m_movement, 1);
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
            canDoubleJump = true;
            m_player.rigidbody2D.velocity = new Vector2(m_player.rigidbody2D.velocity.x, m_player.rigidbody2D.velocity.y + m_jumpHeight);
        }
        else if (Input.GetKeyDown("w") && canDoubleJump)
        {
            canDoubleJump = false;
            m_player.rigidbody2D.velocity = Vector2.zero;
            m_player.rigidbody2D.velocity = new Vector2(m_player.rigidbody2D.velocity.x, m_player.rigidbody2D.velocity.y + m_jumpHeight);
        }
    }

    void Attack()
    {
        print("success!");
        GameObject temp = (GameObject)Instantiate(m_hitBox, new Vector3(m_player.transform.position.x + (m_player.transform.localScale.x * 0.2f), m_player.transform.position.y, m_player.transform.position.z), transform.rotation);
        Physics2D.IgnoreCollision(temp.collider2D, m_player.collider2D);
        Destroy(temp, 0.2f);
    }

    void Summon()
    {
        m_animator.SetInteger("PlayerAnim", 2);

    }

    void Bomb()
    {

    }
}
