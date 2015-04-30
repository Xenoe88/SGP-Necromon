using UnityEngine;
using System.Collections;

public class EvilWizard : MonoBehaviour {

    public GameObject m_target;
    public GameObject m_summonPortal;
    public GameObject m_lightning;

    public AudioClip m_sounds;

    public Entity m_Entity;

    public Animator m_animator;

    public float m_summonTimer = 10.0f;
    public float m_lightTimer = 20.0f;


    public bool m_regen = false;

                public GameObject SFX;

	// Use this for initialization
	void Start () 
    {
        SFX = GameObject.FindGameObjectWithTag("MusicController");

        m_target = GameObject.FindGameObjectWithTag("Player");
        m_Entity = GetComponent<Entity>();
        m_animator = GetComponent<Animator>();
        m_Entity.m_speed = 1;
        m_Entity.m_health = 500;
        m_Entity.m_MaxHealth = 500;

        m_Entity.m_dmg = 50;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (m_Entity.m_health <= 0)
        {
            m_animator.SetInteger("AnimState", 3);
            SFX.GetComponent<LoadSoundFX>().m_soundFXsources["EvilWizardDie"].Play();

            return;
        }
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
                rigidbody2D.isKinematic = true;
                m_animator.SetInteger("AnimState", 2);
            }
            else if (distance > 5)
            {
                rigidbody2D.isKinematic = false;
                m_animator.SetInteger("AnimState", 0);
            }
            if (distance < 10)
            {
                //rigidbody2D.isKinematic = true;
                if (m_summonTimer <= 0.0f)
                {
                    m_animator.SetInteger("AnimState", 1);
                }
                if (m_lightTimer <= 0.0f)
                {
                    m_animator.SetInteger("AnimState", 4);
                }
            }
            else if (distance < 15)
            {
                //rigidbody2D.isKinematic = false;
                Vector3 destination = m_target.transform.position;
                Vector3 direction = (destination - transform.position).normalized;
                transform.Translate(direction * Time.deltaTime, Space.World);
                m_animator.SetInteger("AnimState", 1);
            }
            if (m_summonTimer > 0)
            {
                m_summonTimer -= Time.fixedDeltaTime;
            }
            if (m_lightTimer > 0)
            {
                m_lightTimer -= Time.fixedDeltaTime;
            }
        }
	}

    void PiercingArrows()
    {
        m_target.SendMessage("ModifyHealth", -m_Entity.m_dmg, SendMessageOptions.DontRequireReceiver);
    }

    void Die()
    {

        Destroy(this.gameObject);
        m_target.SendMessage("ModifyGameStatus", SendMessageOptions.DontRequireReceiver);
        Application.LoadLevel("WinScene");
    }

    void Idling()
    {
        m_animator.SetInteger("AnimState", 0);
    }
    void Necromance()
    {
        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["EvilWizardSummon"].Play();
        if (m_Entity.m_health < 100)
        {
            GameObject clone = Instantiate(m_summonPortal, new Vector3(m_target.transform.position.x + 2, m_target.transform.position.y - 0.25f, m_target.transform.position.z), Quaternion.identity) as GameObject;
            GameObject clone1 = Instantiate(m_summonPortal, new Vector3(m_target.transform.position.x, m_target.transform.position.y - 0.25f, m_target.transform.position.z), Quaternion.identity) as GameObject;
            GameObject clone2 = Instantiate(m_summonPortal, new Vector3(m_target.transform.position.x - 2, m_target.transform.position.y - 0.25f, m_target.transform.position.z), Quaternion.identity) as GameObject;
        }
        else
        {
            GameObject clonereg = Instantiate(m_summonPortal, new Vector3(m_target.transform.position.x, m_target.transform.position.y - 0.25f, m_target.transform.position.z), Quaternion.identity) as GameObject;
        }
        m_summonTimer = 10.0f;
    }

    void LightningAttack()
    {
        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["EvilWizardMagicAttack"].Play();

        GameObject clone = Instantiate(m_lightning, m_target.transform.position, Quaternion.identity) as GameObject;
        m_lightTimer = 20.0f;
    }
}
