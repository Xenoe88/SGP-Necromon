﻿using UnityEngine;
using System.Collections;

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

    public enum Status { NONE, STUN, SLOW, CONFUSE };
    public Status m_status;

    public struct StatusMod
    {
        public Status _stat;
        public float _statMod;
        public float _statTimer;
    }
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
