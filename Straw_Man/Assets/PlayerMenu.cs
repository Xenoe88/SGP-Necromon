using UnityEngine;
using System.Collections;

public class PlayerMenu : MonoBehaviour
{
    public int m_necroID;
    private GameObject m_player;
    private GameObject m_selectLocation;

    private Color m_visible;

    // Use this for initialization
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_selectLocation = GameObject.FindGameObjectWithTag("Respawn");
        m_visible = gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_player.GetComponent<PlayerInventory>().runes[m_necroID].collected == false)
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 0.0f);
        else if (m_player.GetComponent<PlayerInventory>().runes[m_necroID].collected == true)
            gameObject.GetComponent<SpriteRenderer>().color = new Color(m_visible.r, m_visible.g, m_visible.b);
    }

    void OnMouseDown()
    {
        if (m_player.GetComponent<PlayerInventory>().runes[m_necroID].collected)
        {
            m_player.GetComponent<PlayerInventory>().m_selectedRune = m_necroID;
            m_selectLocation.SendMessage("Summon", m_necroID, SendMessageOptions.DontRequireReceiver);
        }
    }
}
