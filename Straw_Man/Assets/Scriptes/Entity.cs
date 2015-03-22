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
    public int m_dmg;
    public int m_speed;

    public float m_attackCooldown;
    public float m_statusModifier;
    public float m_statusTimer;

    public bool m_isAlive;

    public Vector3 m_facingDirection;

    public Animator m_animator;


  
    public Texture m_stunTexture, m_slowTexture, m_confuseTexture;

    //public enum Status { NONE, STUN, SLOW, CONFUSE };
    public Status m_status;


	// Use this for initialization
	public void Start () 
    {
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
        if(m_attackCooldown > 0)
        {
            m_attackCooldown -= Time.deltaTime;
            
        }
        if (m_statusTimer > 0)
        {
            m_statusTimer -= Time.deltaTime;
            if (m_statusTimer <= 0)
            {
                m_statusModifier = 1;
                m_status = Status.NONE;
            }
        }
	}

    public void ModifyHealth(int _dmg)
    {
        m_health += _dmg;
    }

    public void ModifyStatus(StatusMod _statusmod)
    {
        m_status = _statusmod._stat;
        m_statusModifier = _statusmod._statMod;
        m_statusTimer += _statusmod._statTimer;
    }
}
