using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour 
{
    public PlayerInventory m_inventory;
    public Entity m_player;
    //public PlayerController m_controller;
    public enum Rank { D, C, B, A };
    public Rank m_rank;

    public int m_gameStatus = 0;

	// Use this for initialization
	void Start () 
    {
        m_rank = Rank.D;
        m_inventory = GetComponent<PlayerInventory>();
        m_player = GetComponent<Entity>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void ModifyRank() { m_rank++; }

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
    }
}
