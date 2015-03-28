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

    public NecroEnemy[] runes;
    public GameObject[] temp;
    //public List<RuneScript> enemiesTEST = new List<RuneScript>();

    public int numBombs = 0;
    public int numRevives = 0;

    //Needs to be an array of pointers to reference the runes 
    public int[] equipped = { 0, 0, 0, 0 };

    public int m_selectedRune;
    public BombScript m_bomb;
    public GameObject player;
    //public RuneScript m_slime;

    // Use this for initialization
    void Start()
    {
        //enemiesTEST.Add(m_slime);

        //sizing a null arrays
        //runes = new RuneScript[14];
        //equipped = new RuneScript[3];
        m_selectedRune = 0;
        runes = new NecroEnemy[temp.Length];
        for (int i = 0; i < temp.Length; i++)
        {
            runes[i].necro = temp[i];
            runes[i].active = false;
            runes[i].collected = false;
        }



        //for (int i = 0; i < 3; i++)
        //    equipped[i] = 4;

        //runes[1] = m_slime;

        //AddRune(1);
    }

    // Update is called once per frame
    void Update()
    {
        
        for (int i = 0; i < runes.Length; i++)
        {
            //runes[i].GetComponent<Entity>().m_ID = 0;
        }
    }

    public void EquipNecro(int _ref)
    {
        if (!runes[_ref].collected)
            return;
        if (_ref < runes.Length)
            for (int i = 0; i < 3; i++)
            {
                if (equipped[i] == 4)
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
                equipped[i] = 4;

            }
        }
    }
    public void Summon(Vector3 pos)
    {
        //if (runes[m_selectedRune].active)
        //    return;

        if (runes[m_selectedRune].active)
            return;

        GameObject temp = (GameObject)Instantiate(runes[m_selectedRune].necro, pos, Camera.main.transform.rotation);
        EnemyActive(m_selectedRune);
        temp.SendMessage("MakeNecro", SendMessageOptions.DontRequireReceiver);
        temp.GetComponent<Entity>().m_NecroCooldown = 10;
        temp.SendMessage("MakeOwner", gameObject, SendMessageOptions.DontRequireReceiver);
    }
    public void Unsummon(int _selected)
    {
       // print(_selected);
       // print(runes[_selected].necro.gameObject);
    
        //runes[_selected].necro = temp[_selected];
        //runes[_selected].active = false;
       // Destroy(runes[_selected].necro.gameObject);

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
        print("HASDFJD");
        if (runes[_necroID].active)
            runes[_necroID].active = false;
        else
            runes[_necroID].active = true;

        print(runes[_necroID].active);

    }
}
