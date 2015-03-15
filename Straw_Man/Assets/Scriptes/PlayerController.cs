using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    //start 2:30 pm
    //pause 3:45 pm
    //start 5:25 pm
    //stop 8:55 pm

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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // left/right
        Move();

        Jump();

        if (Input.GetKeyDown("k"))
            Attack();

    }

    void Move()
    {
        // get direction
        m_movement = Input.GetAxisRaw("Horizontal");

        // turn
        if (m_movement != 0)
            m_player.transform.localScale = new Vector3(m_movement, 1);

        // move
        m_player.rigidbody2D.velocity = new Vector2(m_movement * m_player.GetComponent<Entity>().m_speed * m_player.GetComponent<Entity>().m_statusModifier, m_player.rigidbody2D.velocity.y);
    }

    void Jump()
    {
        m_isGrounded = Physics2D.OverlapCircle(m_groundCheck.position, m_groundRadius, m_whatIsGround);

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
        GameObject temp = (GameObject)Instantiate(m_hitBox, new Vector3(m_player.transform.position.x + (m_player.transform.localScale.x * 0.2f), m_player.transform.position.y, m_player.transform.position.z), transform.rotation);
        Physics2D.IgnoreCollision(temp.collider2D, m_player.collider2D);
        Destroy(temp, 0.2f);
    }

}
