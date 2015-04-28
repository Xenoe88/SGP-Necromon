using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

    public GameObject m_reciever;

	// Use this for initialization
	void Start () 
    {
        m_reciever = GameObject.FindGameObjectWithTag("Receiver");
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerStay2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            if (target.gameObject.GetComponent<PlayerScript>().m_inBossRoom == false)
            {
                target.gameObject.GetComponent<PlayerScript>().m_inBossRoom = true;
            }
            target.transform.position = m_reciever.transform.position;
        }
    }
}
