using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

    public GameObject m_reciever;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerStay2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            target.transform.position = m_reciever.transform.position;
        }
    }
}
