using UnityEngine;
using System.Collections;

public class Necromancer : MonoBehaviour
{

    public GameObject   m_target;
    public GameObject   m_summonPortal;

    public AudioClip    m_sounds;

    public Entity       m_Entity;

    public Animator     m_animator;

    public float        m_summonTimer       =   10.0f;
    public float        m_teleportTimer     =   15.0f;

    public bool         m_regen             =   false;

                public GameObject SFX;


    // Use this for initialization
    void Start()
    {
        SFX = GameObject.FindGameObjectWithTag("MusicController");

        m_target = GameObject.FindGameObjectWithTag("Player");
        m_Entity = GetComponent<Entity>();
        m_animator = GetComponent<Animator>();
        m_Entity.m_speed = 1;
        m_Entity.m_health = 300;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Entity.m_health <= 0)
        {
            m_animator.SetInteger("AnimState", 3);
            return;
        }
        if (m_target != null)
        {
            if (m_target.transform.position.y > (transform.position.y + 1))
            {
                this.rigidbody2D.gravityScale = 0;
                this.gameObject.collider2D.enabled = false;
            }
            else
            {
                this.gameObject.collider2D.enabled = true;
                this.rigidbody2D.gravityScale = 1;
            }
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
                rigidbody2D.isKinematic = true;
                if (m_teleportTimer <= 0.0f)
                {
                    m_animator.SetInteger("AnimState", 5);
                    AudioSource.PlayClipAtPoint(m_sounds, transform.position);
                    m_teleportTimer = 15.0f;
                }
            }
            else if (distance > 5)
            {
                rigidbody2D.isKinematic = false;
            }
            if (distance < 15)
            {
               //rigidbody2D.isKinematic = true;
                if (m_summonTimer <= 0.0f)
                {
                    m_animator.SetInteger("AnimState", 2);
                }
            }
            else if (distance < 20)
            {
                //rigidbody2D.isKinematic = false;
                Vector3 destination = m_target.transform.position;
                Vector3 direction = (destination - transform.position).normalized;
                transform.Translate(direction * Time.deltaTime, Space.World);
                m_animator.SetInteger("AnimState", 1);
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
        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["NecromancerTeleport"].Play();

        float randx = Random.Range(transform.position.x - 5.0f, transform.position.x + 5.0f);
        //float randy = Random.Range(transform.position.y - 10.0f, transform.position.y + 10.0f);
        transform.position = new Vector3(randx, transform.position.y, transform.position.z);
        m_animator.SetInteger("AnimState", 6);
        AudioSource.PlayClipAtPoint(m_sounds, transform.position);
    }
    void Idling()
    {
        m_animator.SetInteger("AnimState", 0);
    }
    void Regen()
    {
        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["NecromancerRevive"].Play();

        m_Entity.m_health += 100;
    }
    void Die()
    {
        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["NecromancerDie"].Play();

        Destroy(this.gameObject);
        m_target.SendMessage("ModifyGameStatus", SendMessageOptions.DontRequireReceiver);
        Application.LoadLevel(12); 
    }
    void Necromance()
    {
        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["NecromancerSummon"].Play();
        if (m_Entity.m_health < 100 && !m_regen)
        {
            GameObject clone = Instantiate(m_summonPortal, new Vector3(m_target.transform.position.x + 2, m_target.transform.position.y - 0.25f, m_target.transform.position.z), Quaternion.identity) as GameObject;
            GameObject clone1 = Instantiate(m_summonPortal, new Vector3(m_target.transform.position.x, m_target.transform.position.y - 0.25f, m_target.transform.position.z), Quaternion.identity) as GameObject;
            GameObject clone2 = Instantiate(m_summonPortal, new Vector3(m_target.transform.position.x - 2, m_target.transform.position.y - 0.25f, m_target.transform.position.z), Quaternion.identity) as GameObject;
            m_animator.SetInteger("AnimState", 4);
            m_regen = true;
        }
        else
        {
            GameObject clonereg = Instantiate(m_summonPortal, new Vector3(m_target.transform.position.x, m_target.transform.position.y - 0.25f, m_target.transform.position.z), Quaternion.identity) as GameObject;
        }
        m_summonTimer = 10.0f;
    }
}
