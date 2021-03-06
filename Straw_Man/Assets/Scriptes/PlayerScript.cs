﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour 
{
    public PlayerInventory m_inventory;
    public Entity m_player;
    //public PlayerController m_controller;
    public enum Rank { D, C, B, A };
    public Rank m_rank;

    public int m_gameStatus = 0;
    public bool m_inBossRoom = false;

    //public string m_resource;
    public Texture[] m_hudSprites;

	// Use this for initialization
	void Start () 
    {

        DontDestroyOnLoad(gameObject);

        m_rank = Rank.D;
        m_inventory = GetComponent<PlayerInventory>();
        m_player = GetComponent<Entity>();

        //m_hudSprites = new Texture[gameObject.GetComponent<PlayerInventory>().runes.Length];

        //if (m_resource != "")
        //    m_hudSprites = Resources.LoadAll<Texture>(m_resource);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void ModifyRank() { m_rank++; }
    void ModifyGameStatus() { m_gameStatus++; }
    void OnGUI()
    {
        //status icons
        switch (m_player.m_status)
        {
            case Status.STUN:
                GUI.DrawTexture(new Rect(m_player.transform.position.x , m_player.transform.position.y, 20, 20), m_player.m_stunTexture);
                break;
            case Status.SLOW:
                GUI.DrawTexture(new Rect(m_player.transform.position.x, m_player.transform.position.y, 20, 20), m_player.m_slowTexture);
                break;
            case Status.CONFUSE:
                GUI.DrawTexture(new Rect(m_player.transform.position.x, m_player.transform.position.y, 20, 20), m_player.m_confuseTexture);
                break;
            default:
                break;
        }

        if (gameObject.GetComponent<PlayerInventory>().m_selectedRune != -1)
        {
            if (gameObject.GetComponent<PlayerInventory>().runes[gameObject.GetComponent<PlayerInventory>().m_selectedRune].collected != false)
            {
                GUI.DrawTexture(new Rect(300, 25, 50, 50), m_hudSprites[gameObject.GetComponent<PlayerInventory>().m_selectedRune]);
            }
        }
    }
}
