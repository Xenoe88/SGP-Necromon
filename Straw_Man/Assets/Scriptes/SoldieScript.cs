﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoldieScript : MonoBehaviour
{
    bool isNecro = false;
    private bool ReadyToAttack = false;
    private bool block = false;

    public GameObject target = null;

    // Use this for initialization
    void Start()
    {

        GetComponent<Entity>().m_dmg = -2;
        GetComponent<Entity>().m_facingDirection = new Vector2(1, 0);
        GetComponent<Entity>().m_speed = 1;
        GetComponent<Entity>().m_health = 100;
        GetComponent<Entity>().m_attackCooldown = 0;

        this.transform.localScale = new Vector3((transform.localScale.x == 1) ? -1 : 1, 1, 1);

        //GetComponent<Entity>().m_animator.SetInteger("AnimState", 1);

        GetComponent<Entity>().m_animator = GetComponent<Animator>();

        //target = GameObject.FindGameObjectWithTag("Player");
    }
    void OnGUI()
    {
        Texture2D bitmapTexture = null;
        //bitmapTexture = (Texture2D)Resources.Load("WhitePixel");

        GUI.DrawTexture(new Rect(150, 25, 250, 75), bitmapTexture);
    }
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Entity>().m_health > 0)
        {
            

            if(Block())
            {
                GetComponent<Entity>().m_animator.SetInteger("AnimState", 3);

            }
            else if (isNecro)
            {
            }
            else if(target)
            {
                if (target.tag == "Player" && GetComponent<Entity>().m_attackCooldown <= 0)
                {
                    GetComponent<Entity>().m_animator.SetInteger("AnimState", 2);
                               
                    GetComponent<Entity>().m_attackCooldown = 30;

                }
                if (GetComponent<Entity>().m_attackCooldown > 0)
                    GetComponent<Entity>().m_attackCooldown -= 1;

            }
            else //if (target)
            {
                rigidbody2D.velocity = new Vector2(-transform.localScale.x, 0) * GetComponent<Entity>().m_speed;
                GetComponent<Entity>().m_animator.SetInteger("AnimState", 1);
            }


        }
        else
        {
            GetComponent<Entity>().m_animator.SetInteger("AnimState", 4);

            //Destroy(gameObject);
        }
    }
    public bool Block()
    {
        float num = Random.Range(0.0f, 1.0f);
       
        if(num > .90f)
            return true;
        return false;
    }
    void ModifyHealth(int _amount)
    {
        GetComponent<Entity>().m_health += _amount;
        if(GetComponent<Entity>().m_health <= 0 )
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D _target)
    {
        target = _target.gameObject;
        if(ReadyToAttack)
        { }
        else
            GetComponent<Entity>().m_animator.SetInteger("AnimState", 2);
    
    }
    //Function called as part of the animation in Unity 
    public void Attack()
    {
       
        ReadyToAttack = true;
        target.SendMessage("ModifyHealth", -GetComponent<Entity>().m_dmg, SendMessageOptions.DontRequireReceiver);

    }

}