using UnityEngine;
using System.Collections;

public class Demon : MonoBehaviour
{

    public bool m_isNecro = false;
    public RuneScript m_rune;
    public int slot = 7;
    public GameObject target;
    // Use this for initialization
    void Start()
    {

        GetComponent<Entity>().m_dmg = -5;
        GetComponent<Entity>().m_facingDirection = new Vector2(-1, 0);
        GetComponent<Entity>().m_speed = 3;
        GetComponent<Entity>().m_health = 70;
        GetComponent<Entity>().m_attackCooldown = 0;

        this.transform.localScale = new Vector3((transform.localScale.x == 1) ? -1 : 1, 1, 1);


        GetComponent<Entity>().m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        rigidbody2D.velocity = new Vector2(-transform.localScale.x, 0) * GetComponent<Entity>().m_speed;
        GetComponent<Entity>().m_animator.SetInteger("AnimState", 1);
    }
    void Attack()
    {
        print("Po");

        target.SendMessage("ModifyHealth", -10, SendMessageOptions.DontRequireReceiver);
    }
    void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider.tag == "Player")
        {
           
            target = _collider.gameObject;
            GetComponent<Entity>().m_animator.SetInteger("AnimState", 2);
        }
    }
    void ModifyHealth(int _dmg)
    {
        GetComponent<Entity>().m_health += _dmg;
        GetComponent<Entity>().m_animator.SetInteger("AnimState", 3);

        if (GetComponent<Entity>().m_health <= 0)
            GetComponent<Entity>().m_animator.SetInteger("AnimState", 4);


    }
    void Destroy()
    {
        int randomVariable = Random.Range(0, 100);

        //TODO - also need to test if we've already collected this enemies rune
        if (randomVariable >= 0 && randomVariable <= 20 && m_isNecro == false)
        {
            GameObject temp = (GameObject)Instantiate(m_rune, transform.position, transform.rotation);
            temp.SendMessage("SetID", slot, SendMessageOptions.DontRequireReceiver);
        }

        if (m_isNecro)
        {
            GetComponent<PlayerInventory>().SendMessage("EnemyActive", slot, SendMessageOptions.DontRequireReceiver);
        }

        Destroy(gameObject);
    }
}
