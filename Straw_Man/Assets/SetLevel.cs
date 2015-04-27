using UnityEngine;
using System.Collections;

public class SetLevel : MonoBehaviour 
{
    private GameObject m_player;
    public string m_levelName;

	// Use this for initialization
	void Start ()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_player.GetComponent<PlayerController>().m_lastLevel = m_levelName;
	}
}
