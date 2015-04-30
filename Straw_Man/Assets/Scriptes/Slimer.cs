using UnityEngine;
using System.Collections;

public class Slimer : MonoBehaviour
{
    public float m_moveTimer, m_groundRadius;
    public Entity m_Slimer;
    public Transform m_startMoveSight, m_endMoveSight, m_groundCheck, m_lookforward, m_start;
    public LayerMask m_whatIsGround;
    private Collider2D m_target;

    public bool m_needCollision = true, m_collision = false, m_isNecro = false, m_isAttacking = false, m_isGrounded, m_doesntNeedCollision = false, m_lookForwardCollision = false;

    private Animator m_animator;

    public GameObject m_hitBox, m_player;
    public GameObject m_rune;
    public int slot = 0;

                public GameObject SFX;

    // Use this for initialization
    void Start()
    {
        SFX = GameObject.FindGameObjectWithTag("MusicController");

        m_Slimer = GetComponent<Entity>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //die
        if (m_Slimer.m_health <= 0)
        {
            m_animator.SetInteger("SlimerAnim", 3);
            return;
        }
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
        {
            m_animator.SetInteger("SlimerAnim", 1);
            SFX.GetComponent<LoadSoundFX>().m_soundFXsources["SlimerJump"].Play();
            
        }
        m_moveTimer -= Time.deltaTime;

        Debug.DrawLine(m_startMoveSight.position, m_endMoveSight.position, Color.green);
        //Debug.DrawLine(m_start.position, m_lookforward.position, Color.red);
    }

    void Move()
    {
        m_collision = Physics2D.Linecast(m_startMoveSight.position, m_endMoveSight.position, 1 << LayerMask.NameToLayer("Ground"));

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
                transform.localScale = new Vector3(-0.4f, 0.4f, 0.4f);
            else if (transform.localScale.x < 0)
                transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
    }

    void Attack()
    {
        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["SlimerAttack"].Play();
        m_target.SendMessage("ModifyHealth", m_Slimer.m_dmg,SendMessageOptions.DontRequireReceiver);
        //print("Attacked!");
        //GameObject temp = (GameObject)Instantiate(m_hitBox, new Vector3(m_Slimer.transform.position.x + (m_Slimer.transform.localScale.x * 0.2f), m_Slimer.transform.position.y, m_Slimer.transform.position.z), transform.rotation);
        //Physics2D.IgnoreCollision(temp.collider2D, m_Slimer.collider2D);
        //Destroy(temp, 0.2f);
    }

    void Destroy()
    {
        int randomVariable = Random.Range(0, 100);

        if (randomVariable >= 0 && randomVariable <= 20 && m_isNecro == false)
        {
            GameObject temp = (GameObject)Instantiate(m_rune, transform.position, transform.rotation);
            temp.SendMessage("SetID", slot, SendMessageOptions.DontRequireReceiver);
            //TODO
        }

        if (m_isNecro)
        {

            m_Slimer.Owner.GetComponent<PlayerInventory>().SendMessage("EnemyActive", slot, SendMessageOptions.RequireReceiver);
           // GetComponent<PlayerInventory>().SendMessage("EnemyActive", m_rune, SendMessageOptions.DontRequireReceiver);
        
        }
        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["SlimerDie"].Play();
      
         Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D _target)
    {
        //just turns the enemy if they hit another enemy
        if (_target.tag == "Enemy" && m_isNecro == false)
        {
            if (transform.localScale.x > 0)
                transform.localScale = new Vector3(-0.4f, 0.4f, 0.4f);
            else if (transform.localScale.x < 0)
                transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
        else if (_target.tag == "Player" && m_isNecro == true)
        {
            if (transform.localScale.x > 0)
                transform.localScale = new Vector3(-0.4f, 0.4f, 0.4f);
            else if (transform.localScale.x < 0)
                transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }


    }

    void OnTriggerStay2D(Collider2D _target)
    {
        if (m_Slimer.m_attackCooldown > 0)
            return;

        m_target = _target;

        if (_target.tag == "Player" && m_isNecro == false)
            m_animator.SetInteger("SlimerAnim", 2);
        else if (_target.tag == "Enemy" && m_isNecro == true)
            m_animator.SetInteger("SlimerAnim", 2);
    }

    public void MakeNecro()
    {
        SFX = GameObject.FindGameObjectWithTag("MusicController");

        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["SlimerBattleCry"].Play();

        m_isNecro = true;
        this.tag = "Player";
    }
}
