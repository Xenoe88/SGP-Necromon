using UnityEngine;
using System.Collections;

public class ExitPlayerMenu : MonoBehaviour 
{
    public GameObject m_player;
	// Use this for initialization
	void Start () 
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_player.GetComponent<PlayerController>().SendMessage("EnterExitMenu", SendMessageOptions.DontRequireReceiver);
            Application.LoadLevel(m_player.GetComponent<PlayerController>().m_lastLevel);
        }
	}
}
