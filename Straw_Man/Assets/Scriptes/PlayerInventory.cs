using UnityEngine;
using System.Collections;

public class PlayerInventory : MonoBehaviour
{

    public RuneScript[] runes;

    public int numBombs = 0;
    public int numRevives = 0;

    //Needs to be an array of pointers to reference the runes 
    public RuneScript[] equipped;

    public RuneScript m_selectedRune;
    public BombScript m_bomb;

    // Use this for initialization
    void Start()
    {
        //sizing a null arrays
        runes = new RuneScript[14];
        equipped = new RuneScript[3];
        m_selectedRune = null;

        for (int i = 0; i < 14; i++)
        {
            runes[i] = null;
        }
        for (int i = 0; i < 3; i++)
            equipped[i] = null;

    }

    // Update is called once per frame
    void Update()
    {
        if (m_selectedRune != null && m_selectedRune.isActive)
        {
            m_selectedRune.cooldown -= Time.deltaTime;
        }
    }

    public void EquipNecro(RuneScript _equip, int place)
    {
        if (place < 3)
            equipped[place] = _equip;
    }

    public void UnEquipeNecro(RuneScript _unEquip, int place)
    {
        if (place < 3)
        {
            if (equipped[place] == _unEquip)
                equipped[place] = null;

            if (equipped[place] == m_selectedRune)
                m_selectedRune = null;
        }
    }

    public void AddRune(RuneScript _rune)
    {
        int ID = _rune.GetID();
        runes[ID] = _rune;

        //if there's no rune selected, select the one we just added
        if (m_selectedRune == null)
            m_selectedRune = _rune;
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

}
