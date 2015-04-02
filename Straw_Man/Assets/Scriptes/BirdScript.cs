using UnityEngine;
using System.Collections;

public class BirdScript : MonoBehaviour
{
    bool isNecro = false;
    public GameObject target = null;
    bool Attacking = false;
    public Vector3 turnPoint;

    public GameObject m_rune;
    public int slot = 5;
    // Use this for initialization
    public void Start()
    {
        turnPoint = transform.position;
        turnPoint.x = turnPoint.x + 4;

        GetComponent<Entity>().m_dmg = -10;
        GetComponent<Entity>().m_facingDirection = new Vector2(-1, 0);
        GetComponent<Entity>().m_speed = 3;
        GetComponent<Entity>().m_health = 40;
        GetComponent<Entity>().m_attackCooldown = 10;
        GetComponent<Entity>().m_animator = GetComponent<Animator>();

    }


    // Update is called once per frame
    void Update()
    {

        if (GetComponent<Entity>().m_health > 0)
        {

            if (target)
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
        Attacking = true;

    }
    public void Turn()
    {
        this.transform.localScale = new Vector3((transform.localScale.x == 1) ? -1 : 1, 1, 1);

    }
    void ModifyHealth(int _amount)
    {
        GetComponent<Entity>().m_health += _amount;
        if (GetComponent<Entity>().m_health <= 0)
            GetComponent<Entity>().m_animator.SetInteger("AnimState", 3);


    }
    void OnTriggerEnter2D(Collider2D _target)
    {
        if (_target.tag != tag && _target.tag != "Platform")
        {
            target = _target.gameObject;
        }
    }
    void OnTriggerStay2D(Collider2D _stay)
    {
        if (_stay.tag != tag && _stay.tag != "Platform")
        {
            GetComponent<Entity>().m_animator.SetInteger("AnimState", 2);
            Attacking = true;
        }
    }
    void OnTriggerExit2D(Collider2D _target)
    {
        if (_target == target)
        {
            GetComponent<Entity>().m_animator.SetInteger("AnimState", 1);
            target = null;
        }
    }

    public void Attack()
    {
        target.SendMessage("ModifyHealth", GetComponent<Entity>().m_dmg, SendMessageOptions.DontRequireReceiver);
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
            // GetComponent<PlayerInventory>().SendMessage("EnemyActive", m_rune, SendMessageOptions.DontRequireReceiver);

        }
        Destroy(gameObject);
    }
}
