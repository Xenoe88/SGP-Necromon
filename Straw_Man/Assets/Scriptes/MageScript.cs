using UnityEngine;
using System.Collections;

public class MageScript : MonoBehaviour
{

    public GameObject target;
    public AudioClip sounds;
    private Entity mage;

    public bool attack;

    // Use this for initialization
    void Start()
    {
        mage = GetComponent<Entity>();
        mage.m_animator = GetComponent<Animator>();
        mage.m_speed = 2;
        mage.m_health = 300;

    }

    // Update is called once per frame
    void Update()
    {
        if (mage.m_health <= 0)
        {
            Die();
            return;
        }
        if (target)
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
            if (distance >= 5)
            {
                Vector3 destination = target.transform.position;
                Vector3 direction = (destination - transform.position).normalized;
                transform.Translate(direction * Time.deltaTime, Space.World);
            }
            if (distance <= 5)
                mage.m_animator.SetInteger("AnimState", 1);
            else if(distance <= 2)
                mage.m_animator.SetInteger("AnimState", 2);

        }

    }

    void Die()
    {
        Destroy(this.gameObject);
        //target.SendMessage("ModifyStatus", SendMessageOptions.DontRequireReceiver);
        Application.LoadLevel(12);
    }
}
