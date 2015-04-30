using UnityEngine;
using System.Collections;

public enum Status { NONE, STUN, SLOW, CONFUSE };
public struct StatusMod
{
    public Status _stat;
    public float _statMod;
    public float _statTimer;
}
public class Entity : MonoBehaviour {

    public int m_health;
    public int m_MaxHealth;
    public int m_dmg;
    public int m_speed;
    public int m_ID;

    public float m_attackCooldown;
    public float m_statusModifier;
    public float m_statusTimer;
    public float m_NecroCooldown;
    private float hitTimer = 0.0f;

    public bool m_isAlive;

    public Vector3 m_facingDirection;

    public Animator m_animator;

    public Texture m_stunTexture, m_slowTexture, m_confuseTexture;

     public GameObject SFX;
     public GameObject HealthBar;
    public GameObject Owner = null;

    //public enum Status { NONE, STUN, SLOW, CONFUSE };
    public Status m_status;

    public void MakeOwner(GameObject _owner)
    {
        Owner = _owner;
    }
	// Use this for initialization
	public void Start () 
    {
        SFX = GameObject.FindGameObjectWithTag("MusicController");
        m_isAlive = true;
	}
   
	// Update is called once per frame
	public void Update () 
    {
        if (m_health <= 0)
        {
            m_isAlive = false;
            m_health = 0;
        }

       //// HEALTH BAR
       // if (m_health < m_MaxHealth)
       //     HealthBar.transform.localScale = new Vector3((float)m_health / (float)m_MaxHealth, HealthBar.transform.localScale.y, HealthBar.transform.localScale.z);
       // else
       //     HealthBar.transform.localScale = new Vector3(0.0f, HealthBar.transform.localScale.y, HealthBar.transform.localScale.z);


        if (hitTimer > 0.0f)
        {
            hitTimer -= Time.deltaTime;
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f);
        }
        else if (hitTimer < 0.0f)
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
        
        if(m_attackCooldown > 0)
        {
            m_attackCooldown -= Time.deltaTime;
            
        }
        if (m_NecroCooldown > 0)
            m_NecroCooldown -= Time.deltaTime;

        if (m_statusTimer > 0)
        {
            m_statusTimer -= Time.deltaTime;
            if (m_statusTimer <= 0)
            {
                m_statusModifier = 1;
                m_status = Status.NONE;
            }
        }
        if (m_attackCooldown > 0)
            m_attackCooldown -= Time.deltaTime;

        if (hitTimer > 0.0f)
        {
            hitTimer -= Time.deltaTime;
        }
        
	}

    public void ModifyHealth(int _dmg)
    {
        m_health += _dmg;

        hitTimer = 0.25f;

        //
        // if (_dmg < 0)
        //   AudioSource.PlayClipAtPoint(m_entityHitSFX, Camera.main.transform.position);
    }

    public void ModifyStatus(StatusMod _statusmod)
    {
        m_status = _statusmod._stat;
        m_statusModifier = _statusmod._statMod;
        m_statusTimer += _statusmod._statTimer;
    }
}
