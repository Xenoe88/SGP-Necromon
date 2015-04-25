using UnityEngine;
using System.Collections;

public class PositionPlayer : MonoBehaviour 
{
    private GameObject m_player;

	// Use this for initialization
	void Start () 
    {
        m_player = GameObject.FindGameObjectWithTag("Player");

        //if the players reload position is 0,0,0 then put him at the start of the level, otherwise put him at his reload position
        if (m_player.gameObject.GetComponent<PlayerController>().m_reLoadPosition == Vector3.zero)
            m_player.gameObject.transform.position = gameObject.transform.position;
        else
        {
            m_player.gameObject.transform.position = m_player.gameObject.GetComponent<PlayerController>().m_reLoadPosition;
            m_player.gameObject.GetComponent<PlayerController>().m_reLoadPosition = Vector3.zero;

        }

        if (m_player.gameObject.GetComponent<PlayerController>().m_inMenu == true)
        {
            m_player.gameObject.GetComponent<PlayerController>().m_inMenu = false;
        }
	}
}
