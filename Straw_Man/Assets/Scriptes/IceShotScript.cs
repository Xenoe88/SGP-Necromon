using UnityEngine;
using System.Collections;

public class IceShotScript : MonoBehaviour
{
    public Vector2 m_direction;
    public float m_spd = 25;
    public Animator m_animator;
    public string m_OwnerTag;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        // transform.position = new Vector3(m_target.x, m_target.y,0);
        if (m_direction.x < 0 && m_direction.y < 0)
            this.transform.localScale = new Vector3(-1, -1, 1);
        else if (m_direction.x < 0)
            this.transform.localScale = new Vector3(-1, 1, 1);
        else if (m_direction.y < 0)
            this.transform.localScale = new Vector3(1, -1, 1);


    }

    // Update is called once per frame
    void Update()
    {
        if (m_direction.x != 0 && m_direction.y != 0)
            rigidbody2D.velocity = new Vector2(transform.localScale.x, transform.localScale.y) * m_spd;
        else if (m_direction.y == 0)
            rigidbody2D.velocity = new Vector2(transform.localScale.x, 0) * m_spd;
        else if (m_direction.x == 0)
            rigidbody2D.velocity = new Vector2(0, transform.localScale.y) * m_spd;

    }
    void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider.gameObject.tag != m_OwnerTag)
        {
            m_animator.SetInteger("AnimState", 1);
            _collider.gameObject.SendMessage("ModifyHealth", -15, SendMessageOptions.DontRequireReceiver);
        }
    }
   
    public void Attack()
    {

    }
    void Destroy()
    {
        Destroy(gameObject);
    }
}
