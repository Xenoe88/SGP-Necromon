using UnityEngine;
using System.Collections;

public class MageScript : MonoBehaviour
{

    public GameObject target;
    public AudioClip sounds;
    private Entity mage;
    public bool attack = false;

    // Use this for initialization
    void Start()
    {
        mage = GetComponent<Entity>();
        mage.m_animator = GetComponent<Animator>();
       mage.m_facingDirection = new Vector2(1, 0);

        mage.m_speed = 2;
        mage.m_health = 300;

    }

    // Update is called once per frame
    void Update()
    {
        if (mage.m_health <= 0)
        {
            mage.m_animator.SetInteger("AnimState", 5);

            return;
        }
        if (target && mage.m_attackCooldown <= 0)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);

            if (target.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (target.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (distance >= 5 && !attack)
            {
                Vector3 destination = target.transform.position;
                Vector3 direction = (destination - transform.position).normalized;
                transform.Translate(direction * Time.deltaTime, Space.World);
            }

            if (distance <= 4)
            {
                attack = true;
                mage.m_attackCooldown = 10;
                mage.m_animator.SetInteger("AnimState", 2);
            }
            else if (distance >= 5 && distance <= 10)
            {
                mage.m_attackCooldown = 5;
                attack = true;
                mage.m_animator.SetInteger("AnimState", 1);
            }

        }
        //else //if(!target)
        //{
        //    int rd = Random.Range(0, 1);
        //    if (rd == 1)
        //        rigidbody2D.velocity = new Vector2(-transform.localScale.x, 0) * GetComponent<Entity>().m_speed;
        //    else if (rd == 0)
        //        rigidbody2D.velocity = new Vector2(0, transform.localScale.y) * GetComponent<Entity>().m_speed;

        //}

    }
    void FireAtk()
    {
        target.SendMessage("ModifyHealth", -20, SendMessageOptions.DontRequireReceiver);
        //target.SendMessage("ModifyStatus", Status.CONFUSE, SendMessageOptions.DontRequireReceiver);
        mage.m_animator.SetInteger("AnimState", 0);

    }
    void IceAtk()
    {
        target.SendMessage("ModifyHealth", -10, SendMessageOptions.DontRequireReceiver);
        //target.SendMessage("ModifyStatus", Status.STUN, SendMessageOptions.DontRequireReceiver);
        mage.m_animator.SetInteger("AnimState", 0);

    }
    void Die()
    {
        Destroy(this.gameObject);
        target.SendMessage("ModifyGameStatus", SendMessageOptions.DontRequireReceiver);
        Application.LoadLevel(12);
    }
}
