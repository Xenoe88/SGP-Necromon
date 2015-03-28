using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public GameObject[] m_necromon;
    public int[] m_equippedNecromon;
    public int m_selectedRune;

    public GameObject m_slimer;

    public BombScript m_bomb;
    public int numBombs = 0;

    public int numRevives = 0;

    // Use this for initialization
    void Start()
    {
        m_necromon = new GameObject[1];
        m_equippedNecromon = new int[3];

        for (int i = 0; i < m_equippedNecromon.Length; i++)
        {
            m_equippedNecromon[i] = -1;
        }
        m_selectedRune = -1;

        m_necromon[0] = m_slimer;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject necromon in m_necromon)
        {
            necromon.GetComponent<NecromonInfo>().m_spawnCooldown -= Time.deltaTime;

            if (necromon.GetComponent<NecromonInfo>().m_spawnCooldown < 0)
                necromon.GetComponent<NecromonInfo>().m_spawnCooldown = 0;

            //if they aren't active and their cooldown has expired, modify the summon variable
            if (necromon.GetComponent<NecromonInfo>().m_isActive == false && necromon.GetComponent<NecromonInfo>().m_spawnCooldown <= 0)
                necromon.GetComponent<NecromonInfo>().ModifyCanSummon();

        }
    }

    public void EquipNecro(GameObject _equip, int _index, int _enemyID)
    {
        if (_index < 3)
            m_equippedNecromon[_index] = _enemyID;
    }

    public void UnEquipeNecro(GameObject _unEquip, int _index, int _enemyID)
    {
        if (_index < 3)
        {
            if (m_equippedNecromon[_index] == _enemyID)
                m_equippedNecromon[_index] = -1;

            if (m_equippedNecromon[_index] == m_selectedRune)
                m_selectedRune = -1;
        }
    }

    public void AddRune(int _enemyID)
    {
        foreach (GameObject enemy in m_necromon)
        {
            if (enemy.GetComponent<NecromonInfo>().m_ID == _enemyID)
            {
                enemy.GetComponent<NecromonInfo>().Collect();

                //this should run the first time we collect a necromon
                if (m_selectedRune == -1)
                {
                    m_equippedNecromon[0] = _enemyID;
                    m_selectedRune = _enemyID;
                }
            }
        }


    }

    public void Select(int _place) { m_selectedRune = m_equippedNecromon[_place]; }

    public int GetNumBomb() { return numBombs; }

    public void AddBomb() { numBombs++; }

    public void UseBomb()
    {
        if (numBombs > 0)
            numBombs--;
    }

    public int GetNumRevives() { return numRevives; }

    public void AddRevive() { numRevives++; }

    public void UseRevives()
    {
        if (numRevives > 0)
            numRevives--;
    }

    public void Summon()
    {
        GameObject temp = (GameObject)Instantiate(m_necromon[m_selectedRune], GetComponent<PlayerController>().m_player.transform.position, Camera.main.transform.rotation);
        temp.SendMessage("MakeNecro", SendMessageOptions.RequireReceiver);
    }
    //public void EnemyInactive(int _necroID)
    //{
    //    //foreach (GameObject enemy in m_necromon)
    //    //{
    //    //    if (enemy.enemyID == _necroID)
    //    //    {
    //    //        enemy.isActive = false;

    //    //        if (enemy.enemyID == m_selectedRune.enemyID)
    //    //            m_selectedRune.isActive = false;
    //    //    }
    //    //}
    //}
}
