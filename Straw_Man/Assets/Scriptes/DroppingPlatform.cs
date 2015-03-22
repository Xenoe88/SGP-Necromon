using UnityEngine;
using System.Collections;

public class DroppingPlatform : MonoBehaviour {

    public bool m_active = false;
    public bool m_drop = false;

    public float m_dropTimer = 2.0f;
    public float m_goneTimer = 5.0f;

    public Vector3 m_yPos;


	// Use this for initialization
	void Start () {
        m_yPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (!m_drop)
        {
            if (m_active)
            {
                m_dropTimer -= Time.fixedDeltaTime;
                if (m_dropTimer <= 0)
                {
                    m_drop = true;
                    m_goneTimer = 5.0f;
                }
            }
        }
        else
        {
            if (transform.position.y > -100.0f)
            {
            transform.Translate(new Vector3(0, transform.position.y, 0) * -2.75f);
            }
            m_goneTimer -= Time.fixedDeltaTime;
            if (m_goneTimer <= 0.0f)
            {
                    transform.position = m_yPos;
                    m_active = false;
                    m_drop = false;
                    m_dropTimer = 2.0f;
                    m_goneTimer = 5.0f;
            }
            
        }
	}

    void OnTriggerStay2D(Collider2D target)
    {
        m_active = true;
    }
}
