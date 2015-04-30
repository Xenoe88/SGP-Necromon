using UnityEngine;
using System.Collections;

public class Wolf : MonoBehaviour
{

    public Transform sightStart, sightEnd;
    public Transform m_groundCheck;

    public Entity m_Entity;

    public bool m_needsCollision = true;
    public bool m_collision = false;
    public bool m_moving = true;
    public bool m_isNecro = false;
    public bool m_isGrounded;

    public Animator m_animator;

    public GameObject m_target;
    public GameObject m_rune;
    public GameObject SFX;

    public int m_arrayIndex = 2;

    public LayerMask m_whatIsGround;

    public float m_groundRadius;

    // Use this for initialization
    void Start()
    {
        SFX = GameObject.FindGameObjectWithTag("MusicController");

        m_Entity = GetComponent<Entity>();
        m_animator = GetComponent<Animator>();
        m_Entity.m_speed = 1;
        m_Entity.m_dmg = 5;
        m_Entity.m_health = 20;
        m_Entity.m_MaxHealth = 20;

        m_Entity.m_attackCooldown = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_isGrounded = Physics2D.OverlapCircle(m_groundCheck.position, m_groundRadius, m_whatIsGround);
        if (!m_isGrounded)
        {
            rigidbody2D.gravityScale = 3;
            m_moving = false;
        }
        else
        {
            rigidbody2D.gravityScale = 1;
            m_moving = true;
        }
        //if (m_Entity.m_isAlive == false)
        //{
        //    m_animator.SetInteger("AnimState", 3);
        //    return;
        //}
        if (m_Entity.m_health <= 0)
        {
            m_animator.SetInteger("AnimState", 3);
            return;
        }
        if (m_target)
        {
            if (m_target.GetComponent<Entity>().m_health <= 0 || m_target.GetComponent<Entity>().m_isAlive == false)
            {
                m_target = null;
                m_moving = true;
            }
        }
        if (m_moving)
        {
            rigidbody2D.velocity = new Vector2(transform.localScale.x, 0) * m_Entity.m_speed;
            m_animator.SetInteger("AnimState", 1);
        }
        //else
        //{
        //    m_animator.SetInteger("AnimState", 0);
        //}
        m_collision = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Ground"));
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.red);
        if (m_target != null)
        {
            float distance = Vector3.Distance(m_target.transform.position, transform.position);
            if (distance < 1)
            {
                m_animator.SetInteger("AnimState", 2);
                m_moving = false;
            }
            else if (distance > 1)
            {
                m_moving = true;
            }
        }
        if (m_target != null && m_moving == true)
        {
            Vector3 destination = m_target.transform.position;
            Vector3 direction = (destination - transform.position).normalized;
            transform.Translate(direction * Time.deltaTime, Space.World);
            m_animator.SetInteger("AnimState", 1);

            if (m_target.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (m_target.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

        }
        else if (m_target == null && m_moving == true)
        {
            rigidbody2D.velocity = new Vector2(transform.localScale.x, 0) * m_Entity.m_speed;
            m_animator.SetInteger("AnimState", 1);
            if (m_collision == m_needsCollision)
            {
                this.transform.localScale = new Vector3((transform.localScale.x == 1) ? -1 : 1, 1, 1);
            }
        }
    }

    void OnTriggerStay2D(Collider2D target)
    {
        if (!m_isNecro && target.tag == "Player")
        {
            m_target = target.gameObject;
        }
        else if (m_isNecro && target.tag == "Enemy")
        {
            m_target = target.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D target)
    {
        m_target = null;
    }
    void Attack()
    {
        m_moving = false;
        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["WolfAttack"].Play();
        m_target.SendMessage("ModifyHealth", -m_Entity.m_dmg, SendMessageOptions.DontRequireReceiver);
        int random = Random.Range(0, 5);
        if (random == 3)
        {
            StatusMod slow;
            slow._stat = Status.SLOW;
            slow._statMod = 0.5f;
            slow._statTimer = 2.0f;
            m_target.SendMessage("ModifyStatus", slow, SendMessageOptions.DontRequireReceiver);
        
        }
    }
    public void Die()
    {
        int randomVariable = 20;//Random.Range(0, 100);
        if (randomVariable >= 0 && randomVariable <= 20 && m_isNecro == false)
        {
            GameObject temp = (GameObject)Instantiate(m_rune, transform.position, transform.rotation);
            temp.SendMessage("SetID", m_arrayIndex, SendMessageOptions.DontRequireReceiver);
            //TODO
        }

        if (m_isNecro)
        {
            m_Entity.Owner.GetComponent<PlayerInventory>().SendMessage("EnemyActive", m_arrayIndex, SendMessageOptions.RequireReceiver);
        }
        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["WolfDie"].Play();

        Destroy(this.gameObject);
    }
    void MakeNecro()
    {
        m_isNecro = true;
        this.tag = "Player";
        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["WolfBattleCry"].Play();
         
    }
}

