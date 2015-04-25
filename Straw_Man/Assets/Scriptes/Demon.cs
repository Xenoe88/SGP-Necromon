using UnityEngine;
using System.Collections;

public class Demon : MonoBehaviour
{
    public Entity m_Entity;

    public bool m_moving = true;
    public bool m_isNecro = false;

    public Animator m_animator;

    public GameObject m_target;
    public GameObject m_rune;

    public int m_arrayIndex = 8;
    // Use this for initialization
    void Start()
    {
        m_Entity = GetComponent<Entity>();
        m_animator = GetComponent<Animator>();
        m_Entity.m_speed = 3;
        m_Entity.m_dmg = 10;
        m_Entity.m_health = 70;
        m_Entity.m_attackCooldown = 1.0f;

        m_animator.SetInteger("AnimState", 1);
        
        this.transform.localScale = new Vector3((transform.localScale.x == 1) ? -1 : 1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Entity.m_health <= 0)
        {
            m_animator.SetInteger("AnimState", 3);
            return;
        }
        if (m_target != null)
        {
            float distance = Vector3.Distance(m_target.transform.position, transform.position);
            if (distance < 3)
            {
                m_animator.SetInteger("AnimState", 2);
                m_moving = false;
            }
            else if (distance > 3)
            {
                m_moving = true;
            }
        }
        if (m_target != null && m_moving == true)
        {
            Vector3 destination = m_target.transform.position;
            Vector3 direction = (destination - transform.position).normalized;
            transform.Translate(direction * Time.deltaTime, Space.World);
            m_animator.SetInteger("AnimState", 1);

            if (m_target.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (m_target.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

        }
        else if (m_target == null && m_moving == true)
        {
            rigidbody2D.velocity = new Vector2(-transform.localScale.x, 0) * m_Entity.m_speed;
            m_animator.SetInteger("AnimState", 1);
        }
        
    }
    void Attack()
    {
        m_Entity.SFX.GetComponent<LoadSoundFX>().m_soundFXsources["DemonAttack"].Play();

        m_target.SendMessage("ModifyHealth", -m_Entity.m_dmg, SendMessageOptions.DontRequireReceiver);
    }

    void OnTriggerExit2D(Collider2D target)
    {
        m_target = null;
    }

    public void MakeNecro()
    {
        m_isNecro = true;
        this.tag = "Player";
        m_Entity.SFX.GetComponent<LoadSoundFX>().m_soundFXsources["DemonBattleCry"].Play();

    }

    void OnTriggerStay2D(Collider2D target)
    {
        if (target.tag == "Player" && !m_isNecro)
        {
            m_target = target.gameObject;
        }
        else if (target.tag == "Enemy" && m_isNecro)
        {
            m_target = target.gameObject;
        }
    }
    //void ModifyHealth(int _dmg)
    //{
    //    GetComponent<Entity>().m_health += _dmg;
    //    GetComponent<Entity>().m_animator.SetInteger("AnimState", 3);
    //}

    //void CheckHealth()
    //{
    //    if (GetComponent<Entity>().m_health <= 0)
    //    {
    //        GetComponent<Entity>().m_animator.SetInteger("AnimState", 5);
    //    }
    //    else
    //        GetComponent<Entity>().m_animator.SetInteger("AnimState", 0);
    //}
    void Destroy()
    {
        int randomVariable = Random.Range(0, 100);

        //TODO - also need to test if we've already collected this enemies rune
        if (randomVariable >= 0 && randomVariable <= 20 && m_isNecro == false)
        {
            GameObject temp = (GameObject)Instantiate(m_rune, transform.position, transform.rotation);
            temp.SendMessage("SetID", m_arrayIndex, SendMessageOptions.DontRequireReceiver);
        }
        
        if (m_isNecro)
        {
            GetComponent<PlayerInventory>().SendMessage("EnemyActive", m_arrayIndex, SendMessageOptions.DontRequireReceiver);
        }
        m_Entity.SFX.GetComponent<LoadSoundFX>().m_soundFXsources["DemonDie"].Play();
        Destroy(this.gameObject);
    }
}
