using UnityEngine;
using System.Collections;

public class Necromancer : MonoBehaviour {

    public GameObject m_target;
    public GameObject m_summonPortal;

    public Entity m_Entity;

    public Animator m_animator;

    public float m_summonTimer = 20.0f;
    public float m_teleportTimer = 30.0f;

	// Use this for initialization
	void Start () {
        m_Entity = GetComponent<Entity>();
        m_animator = GetComponent<Animator>();
        m_Entity.m_speed = 1;
        m_Entity.m_health = 300;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (m_target != null)
        {
            if (m_target.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (m_target.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            float distance = Vector3.Distance(m_target.transform.position, transform.position);
            //print(distance);
            if (distance < 2)
            {
                if (m_teleportTimer <= 0.0f)
                {
                    m_animator.SetInteger("AnimState", 5);
                    m_teleportTimer = 30.0f;
                }
            }

            if (m_summonTimer <= 0.0f)
            {
                GameObject clone = Instantiate(m_summonPortal, new Vector3(m_target.transform.position.x, m_target.transform.position.y - 0.25f, m_target.transform.position.z), Quaternion.identity) as GameObject;
                m_summonTimer = 20.0f;
            }

            if (m_teleportTimer > 0)
            {
                m_teleportTimer -= Time.fixedDeltaTime;
            }
            if (m_summonTimer > 0)
            {
                m_summonTimer -= Time.fixedDeltaTime;
            }
        }
	}

    void Teleport()
    {
        float randx = Random.Range(transform.position.x - 10.0f, transform.position.x + 10.0f);
        //float randy = Random.Range(transform.position.y - 10.0f, transform.position.y + 10.0f);
        transform.position = new Vector3(randx, transform.position.y, transform.position.z);
        m_animator.SetInteger("AnimState", 6);
    }
    void Idling()
    {
        m_animator.SetInteger("AnimState", 0);
    }
}
