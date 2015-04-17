using UnityEngine;
using System.Collections;

public class DisplayNecromon : MonoBehaviour 
{
    public string m_resource;
    public Sprite[] m_hudSprites;
    private GameObject m_player;

	// Use this for initialization
	void Start () 
    {
        m_player = GameObject.FindGameObjectWithTag("Player");

        if (m_resource != "")
        {
            m_hudSprites = Resources.LoadAll<Sprite>(m_resource);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (m_player.GetComponent<PlayerInventory>().m_selectedRune != -1)
        {
            GetComponent<SpriteRenderer>().sprite = m_hudSprites[m_player.GetComponent<PlayerInventory>().m_selectedRune];
        }
	}
}
