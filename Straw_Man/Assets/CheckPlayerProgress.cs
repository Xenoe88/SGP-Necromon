using UnityEngine;
using System.Collections;

public class CheckPlayerProgress : MonoBehaviour 
{
    public int m_sealID;
    public GameObject m_player;

	// Use this for initialization
	void Start () 
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        if (m_player.gameObject.GetComponent<PlayerScript>().m_gameStatus == m_sealID)
        {
            DestroyImmediate(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        //temp
        if (m_player.gameObject.GetComponent<PlayerScript>().m_gameStatus == m_sealID)
        {
            DestroyImmediate(gameObject);
        }
	}
}
