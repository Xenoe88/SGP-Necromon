using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoldieScript : MonoBehaviour
{
    bool isNecro = false;
    bool seeEnemy = false;
    public Transform sightStart, sightEnd;

    private bool ReadyToAttack = false;
    //private bool block = false;

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

       // GUI.DrawTexture(new Rect(150, 25, 250, 75), bitmapTexture);
    }
    // Update is called once per frame
    void Update()
    {

        if (GetComponent<Entity>().m_health > 0)
        {
     

            if (Block())
            {
                GetComponent<Entity>().m_animator.SetInteger("AnimState", 3);

            }
            else if (isNecro)
            {

            }
            else if (target != null)
            {

                if ( GetComponent<Entity>().m_attackCooldown <= 0)
                {
                    GetComponent<Entity>().m_animator.SetInteger("AnimState", 2);
                    GetComponent<Entity>().m_attackCooldown = 5;

                    //GetComponent<Entity>().m_attackCooldown = 30;

                }
               if(target.transform.localScale == transform.localScale)
                   this.transform.localScale = new Vector3((transform.localScale.x == 1) ? -1 : 1, 1, 1);


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

        }
    }
    public bool Block()
    {
        float num = Random.Range(0.0f, 1.0f);

        //if(num > .90f)
        //    return true;
        return false;
    }
    void ModifyHealth(int _amount)
    {
        GetComponent<Entity>().m_health += _amount;
   
    }
    void OnTriggerEnter2D(Collider2D _target)
    {

        if (GetComponent<SeenEnemy>().collision)
        {
            print("test0:");
            target = _target.gameObject;
        }
        else
        {
            if (ReadyToAttack)
            {

            }
            else
            {

            }
       
        }

        //if (Vector3.Distance(transform.position, _target.transform.position) <= 4 && target == null)
        //{
        //    if (isNecro && _target.tag == "Enemy")
        //        target = _target.gameObject;
        //    else if (!isNecro && _target.tag == "Player")
        //        target = _target.gameObject;
        //}
        //else if ( _target.gameObject.tag == "Player")
        //{
        //    ReadyToAttack = true;
        //    GetComponent<Entity>().m_animator.SetInteger("AnimState", 2);
        //}
    }
    void OnTriggerExit2D()
    {
        ReadyToAttack = false;
        GetComponent<Entity>().m_animator.SetInteger("AnimState", 1);

        //if ((target.transform.position.x - transform.position.x > 4))
            target = null;
        //ReadyToAttack = false;
        //seeEnemy = false;


    }

 
    //Function called as part of the animation in Unity 
    public void Attack()
    {

        ReadyToAttack = true;
        target.SendMessage("ModifyHealth", GetComponent<Entity>().m_dmg, SendMessageOptions.DontRequireReceiver);

    }
    public void Death()
    {
        Destroy(gameObject);
    }
}