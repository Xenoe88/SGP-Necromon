using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public struct NecroEnemy
{
    public GameObject necro;
    public bool active;
    public bool collected;
}
public class PlayerInventory : MonoBehaviour
{
    //Inventory rune container - each enemy/rune has a specific s;ot
    public NecroEnemy[] runes;
    //parallel container to modify in Unity
    public GameObject[] temp;

    public int numBombs = 0;
    public int numRevives = 0;

    //Needs to be an array of pointers to reference the runes 
    public int[] equipped = { 0, 0, 0 };

    public int m_selectedRune = -1;

    public BombScript m_bomb;
    public GameObject player;

    // Use this for initialization
    void Start()
    {
 
        runes = new NecroEnemy[temp.Length];
        for (int i = 0; i < temp.Length; i++)
        {
            runes[i].necro = temp[i];
            runes[i].active = false;
            runes[i].collected = false;
        }
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void EquipNecro(int _ref)
    {
        if (!runes[_ref].collected)
            return;
        if (_ref < runes.Length)
            for (int i = 0; i < 3; i++)
            {
                if (equipped[i] == 25)
                {
                    equipped[i] = _ref;
                    return;

                }
            }
        equipped[0] = _ref;

    }

    public void UnEquipeNecro(int _ref)
    {
        for (int i = 0; i < 3; i++)
        {
            if (equipped[i] == _ref)
            {
                equipped[i] = 25;

            }
        }
    }

    
    public void Summon(Vector3 pos)
    {
        print(runes[m_selectedRune].active);
        if (runes[m_selectedRune].active || runes[m_selectedRune].necro.GetComponent<Entity>().m_NecroCooldown > 0)
            return;
        print(runes[m_selectedRune].necro.GetComponent<Entity>().m_NecroCooldown);

        GameObject temp = (GameObject)Instantiate(runes[m_selectedRune].necro, pos, Camera.main.transform.rotation);
        EnemyActive(m_selectedRune);
        temp.SendMessage("MakeNecro", SendMessageOptions.DontRequireReceiver);
        temp.GetComponent<Entity>().m_NecroCooldown = 10;
        temp.SendMessage("MakeOwner", gameObject, SendMessageOptions.DontRequireReceiver);
    }
  
    public void AddRune(int _enemyID)
    {
        print(_enemyID);

        runes[_enemyID].collected = true;
    }

    public void Select(int _place) { m_selectedRune = equipped[_place]; }

    public int GetNumBomb()
    {
        return numBombs;
    }

    public void AddBomb()
    {
        numBombs++;
    }

    public void UseBomb()
    {
        if (numBombs > 0)
            numBombs--;
    }

    public int GetNumRevives()
    {
        return numRevives;
    }

    public void AddRevive()
    {
        numRevives++;
    }

    public void UseRevives()
    {
        if (numRevives > 0)
            numRevives--;
    }
    public void EnemyActive(int _necroID)
    {

        if (runes[_necroID].active)
            runes[_necroID].active = false;
        else
            runes[_necroID].active = true;


    }
}
