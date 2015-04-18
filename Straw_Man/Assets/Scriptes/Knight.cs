using UnityEngine;
using System.Collections;

public class Knight : MonoBehaviour
{
    public AudioClip m_sound;

    public Transform sightStart, sightEnd;

    public Entity m_Entity;

    public bool m_needsCollision = true;
    public bool m_collision = false;
    public bool m_moving = true;
    public bool m_isNecro = false;

    public Animator m_animator;

    public GameObject m_target;
    public GameObject m_rune;

    public int m_arrayIndex = 9;
    // Use this for initialization
    void Start()
    {
        m_Entity = GetComponent<Entity>();
        m_animator = GetComponent<Animator>();
        m_Entity.m_speed = 1;
        m_Entity.m_dmg = 10;
        m_Entity.m_health = 50;
        m_Entity.m_attackCooldown = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Entity.m_health <= 0)
        {
            m_animator.SetInteger("AnimState", 3);
            return;
        }
        m_collision = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Ground"));
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.red);
        if (m_target != null)
        {
            float distance = Vector3.Distance(m_target.transform.position, transform.position);
            if (distance < 1)
            {
                m_animator.SetInteger("AnimState", 1);
                m_moving = false;
            }
            else if (distance > 1)
            {
                m_moving = true;
            }
        }
        if (m_target != null && m_moving == true)
        {
            Vector3 destination = m_target.transform.position;
            Vector3 direction = (destination - transform.position).normalized;
            transform.Translate(direction * Time.deltaTime, Space.World);
            m_animator.SetInteger("AnimState", 2);

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
            m_animator.SetInteger("AnimState", 2);
            if (m_collision == m_needsCollision)
            {
                this.transform.localScale = new Vector3((transform.localScale.x == 1) ? -1 : 1, 1, 1);
            }
        }
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

    public void ModifyHealth(int _dmg)
    {
        int blockChance = Random.Range(1, 100);
        if (blockChance > 1 && blockChance < 30)
        {
            print("blocked");
            AudioSource.PlayClipAtPoint(m_sound, transform.position);
        }
        else { m_Entity.m_health += _dmg; }
    }

    void OnTriggerExit2D(Collider2D target)
    {
        m_target = null;
    }

    void Attack()
    {
        m_target.SendMessage("ModifyHealth", -m_Entity.m_dmg, SendMessageOptions.DontRequireReceiver);
        int knockbackchance = Random.Range(1, 100);
        if (knockbackchance >= 1 && knockbackchance <= 60)
        {
            print("knockback");
            m_target.rigidbody2D.AddForce(new Vector2(-500, 100), ForceMode2D.Force);
        }
    }

    void Die()
    {
        int randomVariable = 20;//Random.Range(0, 100);
        if (randomVariable >= 0 && randomVariable <= 20 && m_isNecro == false)
        {
            GameObject temp = (GameObject)Instantiate(m_rune, transform.position, transform.rotation);
            temp.SendMessage("SetID", m_arrayIndex, SendMessageOptions.DontRequireReceiver);
        }

        if (m_isNecro)
        {
            print("line");
            m_Entity.Owner.GetComponent<PlayerInventory>().SendMessage("EnemyActive", m_arrayIndex, SendMessageOptions.RequireReceiver);
        }
        Destroy(this.gameObject);
    }
    public void MakeNecro()
    {
        m_isNecro = true;
        this.tag = "Player";
    }
}
