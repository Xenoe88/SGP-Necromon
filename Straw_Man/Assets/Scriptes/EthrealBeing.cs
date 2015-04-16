using UnityEngine;
using System.Collections;

public class EthrealBeing : MonoBehaviour
{
    public bool isNecro = false;
    public GameObject target = null;
    bool Attacking = false;

    public GameObject m_rune;
    public int slot = 6;


    // Use this for initialization
    public void Start()
    {
        GetComponent<Entity>().m_dmg = -5;
        GetComponent<Entity>().m_facingDirection = new Vector2(-1, 0);
        GetComponent<Entity>().m_speed = 1;
        GetComponent<Entity>().m_health = 70;
        GetComponent<Entity>().m_attackCooldown = 8;
        GetComponent<Entity>().m_animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {


        if (GetComponent<Entity>().m_health > 0 && target  && !Attacking)
        {
            rigidbody2D.velocity = new Vector2(-transform.localScale.x, 0) * GetComponent<Entity>().m_speed;
            GetComponent<Entity>().m_animator.SetInteger("AnimState", 1);
        }
        else if (Attacking)
        {
            if (target.tag == "Player" && GetComponent<Entity>().m_attackCooldown <= 0)
            {
                GetComponent<Entity>().m_animator.SetInteger("AnimState", 2);

                GetComponent<Entity>().m_attackCooldown = 10;

            }
        }
       


    }
    public void MakeNecro()
    {
        isNecro = true;
        tag = "Player";
    }
    void ModifyHealth(int _amount)
    {
        GetComponent<Entity>().m_health += _amount;
        if( GetComponent<Entity>().m_health <= 0)
            GetComponent<Entity>().m_animator.SetInteger("AnimState",3);
              
       
    }
    void OnTriggerEnter2D(Collider2D _target)
    {
        if (_target.tag != tag && _target.tag != "Platform" && _target.tag != "HitBox" && _target.tag != "Untagged")
        {
            target = _target.gameObject;
        }
    }
    void OnTriggerStay2D(Collider2D _stay)
    {
        float dist = Vector3.Distance(_stay.transform.position, transform.position);

        if (_stay.tag != tag && dist < 1)
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
        if (this != null && target)
        {
            target.SendMessage("ModifyHealth", GetComponent<Entity>().m_dmg, SendMessageOptions.DontRequireReceiver);
        }
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
