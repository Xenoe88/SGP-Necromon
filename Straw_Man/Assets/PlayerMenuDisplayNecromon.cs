using UnityEngine;
using System.Collections;

public class PlayerMenuDisplayNecromon : MonoBehaviour 
{
    public GameObject[] m_Necromon;
    public GameObject m_player, m_currentSelection = null;

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
