using UnityEngine;
using System.Collections;

public class EthrealBeing : MonoBehaviour
{
    bool isNecro = false;
    public GameObject target = null;
    bool Attacking = false;

  


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


        if (GetComponent<Entity>().m_health > 0 && target && !Attacking)
        {
            rigidbody2D.velocity = new Vector2(-transform.localScale.x, 0) * GetComponent<Entity>().m_speed;
            GetComponent<Entity>().m_animator.SetInteger("AnimState", 1);
        }
        else if (Attacking)
        {

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
        Destroy(gameObject);
    }
}
