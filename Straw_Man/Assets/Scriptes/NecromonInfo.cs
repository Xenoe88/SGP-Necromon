using UnityEngine;
using System.Collections;

public class NecromonInfo : MonoBehaviour
{
    public int m_ID;
    public bool m_isActive, m_isCollected, m_canSummon;
    public float m_spawnCooldown;

    //public string m_description;
    //public string m_name;
    //public string m_type;

    // Use this for initialization
    void Start()
    {
        m_isActive = true;
        m_isCollected = m_canSummon = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ModifyActiveStatus() { m_isActive = !m_isActive; }

    public void ModifyCanSummon() { m_canSummon = true; }

    public void Collect() { m_isCollected = true; }

}
