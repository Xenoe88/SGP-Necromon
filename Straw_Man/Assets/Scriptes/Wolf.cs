using UnityEngine;
using System.Collections;

public class Wolf : MonoBehaviour {

    public Transform sightStart, sightEnd;

    public Entity m_Entity;

    public bool m_needsCollision = true;
    public bool m_collision = false;
    public bool m_moving = true;
    public bool m_isNecro = false;

    public Animator m_animator;
    public GameObject m_target;

	// Use this for initialization
	void Start () 
    {
        m_Entity = GetComponent<Entity>();
        m_animator = GetComponent<Animator>();
        m_Entity.m_speed = 1;
        m_Entity.m_dmg = 5;
	}
	
	// Update is called once per frame
	void Update () 
    {
        //if (m_Entity.m_isAlive == false)
        //{
        //    m_animator.SetInteger("AnimState", 3);
        //    return;
        //}
        if (m_moving)
        {
            rigidbody2D.velocity = new Vector2(transform.localScale.x, 0) * m_Entity.m_speed;
            m_animator.SetInteger("AnimState", 1);
        }
        //else
        //{
        //    m_animator.SetInteger("AnimState", 0);
        //}
        m_collision = Physics2D.Linecast(sightStart.position,sightEnd.position, 1 << LayerMask.NameToLayer("Ground"));
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.red);

        if (m_collision == m_needsCollision)
        {
            this.transform.localScale = new Vector3((transform.localScale.x == 1) ? -1 : 1, 1, 1);
        }
        if (m_target != null && m_moving == true)
        {
          
            Vector3 destination = m_target.transform.position;
            Vector3 direction = (destination - transform.position).normalized;
            transform.Translate(direction * Time.deltaTime, Space.World);
        }
	}

    void OnTriggerStay2D(Collider2D target)
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance > 2)
        {
            if (target.tag == "Player")
            {
                m_target = target.gameObject;
            }
            m_moving = true;
        }
        else if (distance < 1)
        {
            if (!m_isNecro)
            {
                if (target.tag == "Player")
                {
                    m_animator.SetInteger("AnimState", 2);
                    m_moving = false;
                    target.SendMessage("ModifyHealth", -m_Entity.m_dmg, SendMessageOptions.DontRequireReceiver);
                    int random = Random.Range(0, 5);
                    if (random == 3)
                    {
                        StatusMod slow;
                        slow._stat = Status.SLOW;
                        slow._statMod = 0.5f;
                        slow._statTimer = 2.0f;
                        target.SendMessage("ModifyStatus", slow, SendMessageOptions.DontRequireReceiver);
                        print("slowed");
                    }
                }
            }
            else
            {
                if (target.tag == "Enemy")
                {
                    m_animator.SetInteger("AnimState", 2);
                    m_moving = false;
                    target.SendMessage("ModifyHealth", m_Entity.m_dmg, SendMessageOptions.DontRequireReceiver);
                    int random = Random.Range(0, 5);
                    if (random == 3)
                    {
                        StatusMod slow;
                        slow._stat = Status.SLOW;
                        slow._statMod = 0.5f;
                        slow._statTimer = 2.0f;
                        target.SendMessage("ModifyStatus", slow, SendMessageOptions.DontRequireReceiver);
                        //print("slowed");
                    }
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D target)
    {
        m_target = null;
    }
}

