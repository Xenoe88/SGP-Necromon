using UnityEngine;
using System.Collections;

public class PlayerMenuDisplayNecromon : MonoBehaviour
{
    public GameObject[] m_Necromon;
    public GameObject m_player, m_currentSelection = null;
    public int m_currentNecro = -1;

    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");

        if (m_player.GetComponent<PlayerInventory>().m_selectedRune != -1)
        {
            Summon(m_player.GetComponent<PlayerInventory>().m_selectedRune);
            m_currentNecro = m_player.GetComponent<PlayerInventory>().m_selectedRune;
        }
    }

    void Update()
    {
        if (m_currentNecro != m_player.GetComponent<PlayerInventory>().m_selectedRune)
        {
            m_currentNecro = m_player.GetComponent<PlayerInventory>().m_selectedRune;
            Summon(m_currentNecro);
        }
    }

    public void Summon(int _ID)
    {
        if (m_currentSelection != null)
        {
            Destroy(m_currentSelection);
            m_currentSelection = null;
        }
        m_currentSelection = (GameObject)Instantiate(m_Necromon[_ID], transform.position, Quaternion.identity);
    }
}
