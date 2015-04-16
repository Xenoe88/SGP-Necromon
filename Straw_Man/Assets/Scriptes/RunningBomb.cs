﻿using UnityEngine;
using System.Collections;

public class RunningBomb : MonoBehaviour
{

    public Transform sightStart, sightEnd;

    public Entity m_Entity;

    public bool m_needsCollision = true;
    public bool m_collision = false;
    public bool m_moving = true;
    public bool m_isNecro = false;

    public Animator m_animator;

    public AudioClip m_sound;

    public GameObject m_target;
    public GameObject m_rune;
    public int slot = 4;

    public int m_arrayIndex = 4;

    // Use this for initialization
    void Start()
    {
        m_Entity = GetComponent<Entity>();
        m_animator = GetComponent<Animator>();
        m_Entity.m_speed = 1;
        m_Entity.m_dmg = 20;
        m_Entity.m_health = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Entity.m_health <= 0)
        {
            Destroy(this.gameObject);
        }
        if (m_moving)
        {
            rigidbody2D.velocity = new Vector2(-transform.localScale.x, 0) * m_Entity.m_speed;
            m_animator.SetInteger("AnimState", 0);
        }
        m_collision = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Ground"));
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.red);

        if (m_collision == m_needsCollision)
        {
            this.transform.localScale = new Vector3((transform.localScale.x == 1) ? -1 : 1, 1, 1);
        }
        if (m_target != null && m_moving == true)
        {
            float distance = Vector3.Distance(m_target.transform.position, transform.position);
            if (distance < 3 && m_target.transform.position.y <= transform.position.y)
            {
                Vector3 destination = m_target.transform.position;
                Vector3 direction = (destination - transform.position).normalized;
                transform.Translate(direction * Time.deltaTime, Space.World);
            }
        }
    }

    void OnTriggerStay2D(Collider2D target)
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance > 1)
        {
            if (!m_isNecro && target.tag == "Player")
            {
                //print("hi");
                m_target = target.gameObject;
            }
            else if (m_isNecro && target.tag == "Enemy")
            {
                //print("hi");
                m_target = target.gameObject;
            }
            m_moving = true;
        }
        else if (distance < 1.0f)
        {
            if (!m_isNecro)
            {
                if (target.tag == "Player")
                {
                    m_animator.SetInteger("AnimState", 1);
                    m_moving = false;
                }
            }
            else
            {
                this.tag = "Player";
                if (target.tag == "Enemy")
                {
                    m_animator.SetInteger("AnimState", 1);
                    m_moving = false;
                }
            }
        }
    }
    //void OnTriggerExit2D(Collider2D target)
    //{
    //    m_target = null;
    //}
    public void Attack()
    {
        m_target.SendMessage("ModifyHealth", -m_Entity.m_dmg, SendMessageOptions.DontRequireReceiver);
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
        Destroy(this.gameObject);
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

            m_Entity.Owner.GetComponent<PlayerInventory>().SendMessage("EnemyActive", slot, SendMessageOptions.RequireReceiver);
            // GetComponent<PlayerInventory>().SendMessage("EnemyActive", m_rune, SendMessageOptions.DontRequireReceiver);

        }

        Destroy(gameObject);
    }
    public void MakeNecro()
    {
        m_isNecro = true;
        this.tag = "Player";
    }

    public void Explodesound()
    {
        AudioSource.PlayClipAtPoint(m_sound, transform.position);
    }
}
