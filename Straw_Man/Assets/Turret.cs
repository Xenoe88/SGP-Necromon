using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

    public Entity m_Entity;

    public bool m_moving = true;
    public bool m_isNecro = false;
    public int m_facing = 1;

    public GameObject m_target;
    public Projectile m_proj;
	// Use this for initialization
	void Start () {
        m_Entity = GetComponent<Entity>();
        m_Entity.m_dmg = 10;
        m_Entity.m_health = 20;
        m_Entity.m_attackCooldown = 4.0f;
        m_Entity.m_speed = 8;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (m_Entity.m_health <= 0)
        {
            Destroy(this.gameObject);
        }
        if (m_target == null)
        {
            return;
        }
        else
        {
            //Vector2 m_wayPT = new Vector2(m_target.transform.position.x - transform.position.x, m_target.transform.position.y - transform.position.y);
            ////float ComLength = Mathf.Sqrt((m_wayPT.x * m_wayPT.x) + (m_wayPT.y * m_wayPT.y));
            //m_wayPT.Normalize();
            //// Compute Dot Product
            //float DotProduct = ((m_wayPT.x * transform.rotation.x) + (m_wayPT.y * transform.rotation.y));
            //if (DotProduct < 0.999f)
            //{
            //    transform.Rotate(new Vector3(m_target.transform.position.x, m_target.transform.position.y, 0) * Time.deltaTime * m_Entity.m_speed);
            //}
            //Quaternion m_rotation = Quaternion.LookRotation(new Vector3(m_target.transform.position.x, m_target.transform.position.y, m_target.transform.position.z)+ new Vector3(transform.position.x, transform.position.y, transform.position.z));
            //transform.rotation = Quaternion.Slerp(new Quaternion(transform.rotation.x, transform.rotation.y, 0, 0), new Quaternion(m_rotation.x, m_rotation.y,0,0), Time.deltaTime * m_Entity.m_speed);
            if (m_target.transform.position.x > transform.position.x)
            {
                m_facing = -1;
                this.transform.localScale = new Vector3(m_facing, 1, 1);
            }
            else if (m_target.transform.position.x < transform.position.x)
            {
                m_facing = 1;
                this.transform.localScale = new Vector3(m_facing, 1, 1);
            }
        }
        if (m_Entity.m_attackCooldown < 0.0f)
        {
            Attack();
            m_Entity.m_attackCooldown = 4.0f;
        }
        m_Entity.m_attackCooldown -= Time.deltaTime;
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
        m_Entity.m_attackCooldown = 4.0f;
        m_target = null;
    }

    void Attack()
    {
        if (m_proj)
        {
            Projectile clone = Instantiate(m_proj, transform.position, Quaternion.identity) as Projectile;
            Physics2D.IgnoreCollision(clone.collider2D, this.collider2D);
            clone.rigidbody2D.velocity = new Vector3(-m_facing,0,0) * m_Entity.m_speed;
        }
    }

    void ModifyHealth(int _dmg)
    {
        m_Entity.m_health += _dmg;
    }
}
