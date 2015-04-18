using UnityEngine;
using System.Collections;

public class PlayerMenuSetPlayerLocation : MonoBehaviour
{

    private GameObject m_player;

    // Use this for initialization
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");

        m_player.gameObject.transform.position = gameObject.transform.position;
        m_player.gameObject.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
    }
}