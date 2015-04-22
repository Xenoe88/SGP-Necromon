using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoldieScript : MonoBehaviour
{
    public bool isNecro = false;
    private bool ReadyToAttack = false;
    //private bool block = false;

    public GameObject target = null;
    public GameObject summoner = null;

    public GameObject m_rune;
    public int slot = 3;
    // Use this for initialization
    void Start()
    {

        GetComponent<Entity>().m_dmg = -2;
        GetComponent<Entity>().m_facingDirection = new Vector2(1, 0);
        GetComponent<Entity>().m_speed = 1;
        GetComponent<Entity>().m_health = 100;
        GetComponent<Entity>().m_attackCooldown = 0;

        this.transform.localScale = new Vector3((transform.localScale.x == 1) ? -1 : 1, 1, 1);


        GetComponent<Entity>().m_animator = GetComponent<Animator>();

    }
    void OnGUI()
    {
        //Texture2D bitmapTexture = null;
        //bitmapTexture = (Texture2D)Resources.Load("WhitePixel");

        //GUI.DrawTexture(new Rect(150, 25, 250, 75), bitmapTexture);
    }
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Entity>().m_health > 0)
        {
             if (isNecro)
            {
                summoner = GameObject.FindGameObjectWithTag("Player");
            }
            if(summoner)
            {
                if(Mathf.Abs(Vector3.Distance(transform.localPosition, summoner.transform.localPosition)) > 5)
                {
                    target = summoner;
                }
            }
            else if (target)
            {
                FollowTarget();
           
                if ( GetComponent<Entity>().m_attackCooldown <= 0 && Mathf.Abs(Vector3.Distance(target.gameObject.transform.position, this.transform.position)) < 1  && target.tag != this.tag)
                {
                   GetComponent<Entity>().m_animator.SetInteger("AnimState", 2);
                   KnockBack();
                   GetComponent<Entity>().m_attackCooldown = 2;
                }
             
            }
            else 
            {
                GetComponent<Entity>().m_animator.SetInteger("AnimState", 1);
            }

             rigidbody2D.velocity = new Vector2(-transform.localScale.x, 0) * GetComponent<Entity>().m_speed;

        }
        else
            GetComponent<Entity>().m_animator.SetInteger("AnimState", 4);
    }
    void FollowTarget()
    {
             if ((target.transform.position.x < transform.position.x) )
                    transform.localScale = new Vector3( 1, 1, 1);
             if ((target.transform.position.x > transform.position.x) )
                    transform.localScale = new Vector3(-1, 1, 1);
            
    }
    void KnockBack()
    {
        float num = Random.Range(0.0f, 1.0f);

        if (num > .80f)
        {
            target.gameObject.transform.localPosition = target.gameObject.transform.localPosition + (new Vector3(1.0f, 0.2f, 0.0f) * target.transform.localScale.x);
        }
    }
    public void Die()
    {
        int randomVariable = Random.Range(0, 100);

        if (randomVariable >= 0 && randomVariable <= 20 && isNecro == false)
        {
            GameObject temp = (GameObject)Instantiate(m_rune, transform.position, transform.rotation);
            temp.SendMessage("SetID", slot, SendMessageOptions.DontRequireReceiver);
        }

        if (isNecro)
        {
            GetComponent<Entity>().Owner.GetComponent<PlayerInventory>().SendMessage("EnemyActive", slot, SendMessageOptions.RequireReceiver);
            GetComponent<PlayerInventory>().SendMessage("EnemyActive", m_rune, SendMessageOptions.DontRequireReceiver);
        }

        Destroy(gameObject);
    }
    public bool Block()
    {
        float num = Random.Range(0.0f, 1.0f);

        if (num > .90f)
            return true;
        return false;
    }
    void ModifyHealth(int _amount)
    {
        if (Block())
        {
            GetComponent<Entity>().m_animator.SetInteger("AnimState", 3);
            return;
        }

        GetComponent<Entity>().m_animator.SetInteger("AnimState", 2);
        GetComponent<Entity>().m_health += _amount;
    }
    void OnTriggerEnter2D(Collider2D _target)
    {
        if((_target.gameObject.tag =="Player" || _target.gameObject.tag == "Enemy") && _target.gameObject.tag != this.tag)
         target = _target.gameObject;
     
    }
    void OnTriggerExit2D()
    {
        if(target.gameObject.tag != this.tag)
        target = null;
    }
    //Function called as part of the animation in Unity 
    public void Attack()
    {
        target.SendMessage("ModifyHealth", GetComponent<Entity>().m_dmg, SendMessageOptions.DontRequireReceiver);
        GetComponent<Entity>().m_animator.SetInteger("AnimState", 0);
    }

    public void MakeNecro()
    {
        isNecro = true;
        this.tag = "Player";
    }

}
