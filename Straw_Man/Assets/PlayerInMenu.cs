using UnityEngine;
using System.Collections;

public class PlayerInMenu : MonoBehaviour 
{
    private GameObject m_player;
	// Use this for initialization
	void Start () 
    {
        m_player = GameObject.FindGameObjectWithTag("Player");

        m_player.GetComponent<PlayerController>().m_inMenu = true;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}
}
