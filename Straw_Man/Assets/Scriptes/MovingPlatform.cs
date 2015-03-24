using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    public float m_speed;
    public GameObject m_right, m_left;
    public float m_rightPos, m_leftPos;

	// Use this for initialization
	void Start () 
    {
        m_speed = 0.025f;
        m_rightPos = m_right.transform.position.x;
        m_leftPos = m_left.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.Translate(new Vector3( m_speed, 0, 0), Space.World);

        if (transform.position.x > m_rightPos)
        {
            print("turn left");
            m_speed = -0.025f;
        }
        else if (transform.position.x < m_leftPos)
        {
            print("turn right");
            m_speed = 0.025f;
        }
	}

    void OnTriggerStay2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            target.transform.Translate(new Vector3(m_speed, 0, 0), Space.World);
            //target.transform.Translate(new Vector3(transform.position.x, 0, 0) * m_speed, Space.World);
            //target.transform.position = new Vector3(transform.position.x, target.transform.position.y, target.transform.position.z);
            //target.rigidbody2D.velocity = transform.rigidbody2D.velocity;
        }
    }
}
