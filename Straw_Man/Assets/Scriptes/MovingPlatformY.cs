using UnityEngine;
using System.Collections;

public class MovingPlatformY : MonoBehaviour {

    public float m_speed;
    public float m_botPos, m_topPos;

    public GameObject m_bot, m_top;
	// Use this for initialization
	void Start () {
        m_speed = 0.025f;
        m_botPos = m_bot.transform.position.y;
        m_topPos = m_top.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(0, m_speed, 0), Space.World);

        if (transform.position.y < m_botPos)
        {
         
            m_speed = 0.025f;
        }
        else if (transform.position.y > m_topPos)
        {
          
            m_speed = -0.025f;
        }
	}
}
