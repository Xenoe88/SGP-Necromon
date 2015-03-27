using UnityEngine;
using System.Collections;

public class Slimer : MonoBehaviour
{
    public float m_moveTimer, m_groundRadius;
    public Entity m_Slimer;
    public Transform m_startMoveSight, m_endMoveSight, m_groundCheck;
    public LayerMask m_whatIsGround;
    private Collider2D m_target;

    public bool m_needCollision = true, m_collision = false, m_isNecro = false, m_isAttacking = false, m_isGrounded;

    private Animator m_animator;

    public GameObject m_hitBox, m_rune;

    // Use this for initialization
    void Start()
    {
        m_Slimer = GetComponent<Entity>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //die
        if (m_Slimer.m_isAlive == false)
            m_animator.SetInteger("SlimerAnim", 3);

        //if we're attacking, don't run the rest of the update
        //if (m_isAttacking)
        //    return;

        m_isGrounded = Physics2D.OverlapCircle(m_groundCheck.position, m_groundRadius, m_whatIsGround);

        //zero out his velocity once he lands to he doesn't hop and/or roll
        if (m_isGrounded)
        {
            m_animator.SetInteger("SlimerAnim", 0);
            rigidbody2D.velocity = new Vector2(0.0f, 0.0f);
        }

        //attack called inside OnTriggerEnter2D()

        //move, animation calls function
        if (m_moveTimer <= 0 && m_isGrounded)
            m_animator.SetInteger("SlimerAnim", 1);

        m_moveTimer -= Time.deltaTime;
    }

    void Move()
    {
        m_collision = Physics2D.Linecast(m_startMoveSight.position, m_endMoveSight.position, 1 << LayerMask.NameToLayer("Ground"));
        Debug.DrawLine(m_startMoveSight.position, m_endMoveSight.position, Color.green);

        //we can jump forward
        if (m_needCollision == m_collision)
        {
            //which way should we aim the jump?
            if (transform.localScale.x < 0)
                rigidbody2D.AddForce(new Vector2(100, 100));
            else if (transform.localScale.x > 0)
                rigidbody2D.AddForce(new Vector2(-100, 100));

            //reset the timer after we jump
            m_moveTimer = 3.0f;
        }
        else //we need to turn around, but still need to jump so don't reset the timer yet...maybe?
        {
            if (transform.localScale.x > 0)
                transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f);
            else if (transform.localScale.x < 0)
                transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
    }

    void Attack()
    {
        m_target.SendMessage("ModifyHealth", m_Slimer.m_dmg);
        //print("Attacked!");
        //GameObject temp = (GameObject)Instantiate(m_hitBox, new Vector3(m_Slimer.transform.position.x + (m_Slimer.transform.localScale.x * 0.2f), m_Slimer.transform.position.y, m_Slimer.transform.position.z), transform.rotation);
        //Physics2D.IgnoreCollision(temp.collider2D, m_Slimer.collider2D);
        //Destroy(temp, 0.2f);
    }

    void Destroy() 
    {
        int randomVariable = Random.Range(0, 100);

        if (randomVariable >= 0 && randomVariable <= 20)
        {
            Instantiate(m_rune, transform.position, transform.rotation);

            //TODO
            //m_rune.SendMessage("")
        }
            
        Destroy(gameObject);
    }

    void OnTriggerStay2D(Collider2D _target)
    {
        //print("trigger");
        if (m_Slimer.m_attackCooldown > 0)
            return;

        m_target = _target;
        print(m_target.tag);

        //m_isAttacking = true;
        if (_target.tag == "Player" && m_isNecro == false)
            m_animator.SetInteger("SlimerAnim", 2);
        else if (_target.tag == "Enemy" && m_isNecro == true)
            m_animator.SetInteger("SlimerAnim", 2);
    }

    //void OnTriggerExit2D(Collider2D _target)
    //{
    //    //print("attack false");
    //    m_isAttacking = false;

    //    m_Slimer.m_attackCooldown = 2.0f;
    //}
}
