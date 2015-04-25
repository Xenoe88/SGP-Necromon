using UnityEngine;
using System.Collections;

public class ExitPlayerMenu : MonoBehaviour 
{
    private GameObject m_player;
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
            //GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<LoadingScreen>().ChangeLevel(m_player.GetComponent<PlayerController>().m_lastLevel);

            Application.LoadLevel(m_player.GetComponent<PlayerController>().m_lastLevel);
        }
	}
}
