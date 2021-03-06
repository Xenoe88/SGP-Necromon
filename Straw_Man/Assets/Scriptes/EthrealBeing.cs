﻿using UnityEngine;
using System.Collections;

public class EthrealBeing : MonoBehaviour
{
    public bool isNecro = false;

    public Entity m_Ethreal;

    public GameObject target = null;
    bool Attacking = false;
    public bool phased;

    public GameObject m_rune;
    public int slot = 6;
    private float intangibleTimer;
    private Color phaseColor, startColor;

                public GameObject SFX;


    // Use this for initialization
    public void Start()
    {

        SFX = GameObject.FindGameObjectWithTag("MusicController");

        GetComponent<Entity>().m_dmg = -5;
        GetComponent<Entity>().m_facingDirection = new Vector2(-1, 0);
        GetComponent<Entity>().m_speed = 1;
        GetComponent<Entity>().m_health = 70;
        GetComponent<Entity>().m_MaxHealth = 70;

        GetComponent<Entity>().m_attackCooldown = 8;
        GetComponent<Entity>().m_animator = GetComponent<Animator>();

        startColor = GetComponent<SpriteRenderer>().color;
        phaseColor = new Color(startColor.r, startColor.g, startColor.b, .50f);

        phased = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (GetComponent<Entity>().m_health > 0)
        {

            if (Random.Range(0.0f, 1.0f) > .90f && intangibleTimer <= 0 && Attacking == false)
            {
                Intangible();
                intangibleTimer = 2.0f;
            }

            if (intangibleTimer > 0.0f)
            {
                intangibleTimer -= Time.deltaTime;
            }
            else if (intangibleTimer <= 0.0f)
            {
                Intangible();
            }
            if (target)
            {
                FollowTarget();

                if (target && phased == false)
                {

                    if (GetComponent<Entity>().m_attackCooldown <= 0 && Mathf.Abs(Vector3.Distance(target.gameObject.transform.position, this.transform.position)) < 1)
                    {

                        GetComponent<Entity>().m_animator.SetInteger("AnimState", 2);
                        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["EtheralBeingAttack"].Play();
                        GetComponent<Entity>().m_attackCooldown = 2;

                        Attacking = true;
                    }
                }

            }

            else if (target == null)

                GetComponent<Entity>().m_animator.SetInteger("AnimState", 1);

            rigidbody2D.velocity = new Vector2(-transform.localScale.x, 0) * GetComponent<Entity>().m_speed;
        }
        else
        {
            SFX.GetComponent<LoadSoundFX>().m_soundFXsources["EtheralBeingDie"].Play();

            GetComponent<Entity>().m_animator.SetInteger("AnimState", 3);
        }  
    }





    void Intangible()
    {

        if (phased == false)
        {
            renderer.material.color = phaseColor;
            phased = true;

        }
        else
        {
            renderer.material.color = startColor;

            phased = false;
        }
    }
    void FollowTarget()
    {
        if ((target.transform.position.x < transform.position.x))
            transform.localScale = new Vector3(1, 1, 1);
        if ((target.transform.position.x > transform.position.x))
            transform.localScale = new Vector3(-1, 1, 1);

    }
    public void MakeNecro()
    {
        isNecro = true;
        tag = "Player";

        SFX = GameObject.FindGameObjectWithTag("MusicController");
        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["EtheralBeingBattleCry"].Play();

    }
    void ModifyHealth(int _amount)
    {
        if (phased)
        {
            return;
        }
        GetComponent<Entity>().m_health += _amount;
        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["EtheralBeingTakeDamage"].Play();

     


    }
    void OnTriggerEnter2D(Collider2D _target)
    {
        if (_target.tag != tag && _target.tag != "Platform" && _target.tag != "HitBox" && _target.tag != "Untagged")
        {
            target = _target.gameObject;
        }
    }
    //void OnTriggerStay2D(Collider2D _stay)
    //{
    //    float dist = Vector3.Distance(_stay.transform.position, transform.position);

    //    if (_stay.tag != tag && dist < 1)
    //    {
    //        GetComponent<Entity>().m_animator.SetInteger("AnimState", 2);
    //        Attacking = true;
    //    }
    //}
    void OnTriggerExit2D(Collider2D _target)
    {

        if (target)
        {
            GetComponent<Entity>().m_animator.SetInteger("AnimState", 1);
            target = null;
        }
    }

    public void Attack()
    {

        target.SendMessage("ModifyHealth", GetComponent<Entity>().m_dmg, SendMessageOptions.DontRequireReceiver);
        GetComponent<Entity>().m_animator.SetInteger("AnimState", 1);

        Attacking = false;

    }
    public void Die()
    {
        int randomVariable = Random.Range(0, 100);

        if (randomVariable >= 0 && randomVariable <= 20 && isNecro == false)
        {
            GameObject temp = (GameObject)Instantiate(m_rune, transform.position, transform.rotation);
            temp.SendMessage("SetID", slot, SendMessageOptions.DontRequireReceiver);
            //TODO
        }

        if (isNecro)
        {

            GetComponent<Entity>().Owner.GetComponent<PlayerInventory>().SendMessage("EnemyActive", slot, SendMessageOptions.RequireReceiver);

        }

        Destroy(gameObject);
    }
}
   
