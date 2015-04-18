using UnityEngine;
using System.Collections;

public class Bats : MonoBehaviour
{
    private Animator m_animator;

    public Transform m_leftBarrier, m_rightBarrier, m_topBarrier, m_bottomBarrier;
    public float m_rightPos, m_leftPos, m_topPos, m_bottomPos;

    public GameObject m_rune;
    public bool m_isNecro = false;
    private int m_necroSlot = 10;

    // Use this for initialization
    void Start()
    {
        m_animator = gameObject.GetComponent<Animator>();

        m_rightPos = m_rightBarrier.transform.position.x;
        m_leftPos = m_leftBarrier.transform.position.x;

        m_topPos = m_topBarrier.transform.position.y;
        m_bottomPos = m_bottomBarrier.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Entity>().m_isAlive == false)
        {
            m_animator.SetInteger("BatAnim", 1);
            return;
        }

        Patrol();
    }

    void Patrol()
    {
        int randomVariable = Random.Range(-10, 10);

        if (randomVariable >= -1 && randomVariable <= 1)
            gameObject.rigidbody2D.AddForce(new Vector2(0.0f, randomVariable * 1.0f));
        else
            transform.Translate(new Vector3(gameObject.GetComponent<Entity>().m_speed * gameObject.transform.localScale.x * 0.05f,0, 0), Space.World);

        if (transform.position.x > m_rightPos)
        {
            gameObject.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (transform.position.x < m_leftPos)
        {
            gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        if (transform.position.y > m_topPos)
        {
            transform.Translate(new Vector3(gameObject.GetComponent<Entity>().m_speed * gameObject.transform.localScale.x * 0.05f, -0.001f, 0), Space.World);
        }
        else if (transform.position.y < m_bottomPos)
        {
            transform.Translate(new Vector3(gameObject.GetComponent<Entity>().m_speed * gameObject.transform.localScale.x * 0.05f, 0.001f, 0), Space.World);
        }
    }

    void Attack()
    {

    }

    void Die()
    {
        int randomVariable = Random.Range(0, 100);

        if (randomVariable >= 0 && randomVariable <= 20 && m_isNecro == false)
        {
            GameObject temp = (GameObject)Instantiate(m_rune, transform.position, transform.rotation);
            temp.SendMessage("SetID", m_necroSlot, SendMessageOptions.DontRequireReceiver);
        }

        if (m_isNecro)
            gameObject.GetComponent<Entity>().Owner.gameObject.GetComponent<PlayerInventory>().SendMessage("EnemyActive", m_necroSlot, SendMessageOptions.RequireReceiver);

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D _target)
    {

    }
}
