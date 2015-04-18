using UnityEngine;
using System.Collections;

public class SetLevel : MonoBehaviour 
{
    public GameObject m_player;
    public int m_levelID;

	// Use this for initialization
	void Start () 
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_player.GetComponent<PlayerController>().m_lastLevel = m_levelID;
	}
}
