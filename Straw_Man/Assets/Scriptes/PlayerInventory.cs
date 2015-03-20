using UnityEngine;
using System.Collections;

public class PlayerInventory : MonoBehaviour 
{
    
    public RuneScript[] runes;

    private int numBombs = 0;
    private int numRevives = 0;

   //Needs to be an array of pointers to reference the runes 
    public RuneScript[] equipped;

	// Use this for initialization
	void Start () 
    {
        //sizing a null arrays
        runes = new RuneScript[14];
        equipped = new RuneScript[3];

        for (int i = 0; i < 14; i++)
        {
            runes[i] = null;
        }
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

   public void EquipNecro(RuneScript _equip)
    {
        for (int i = 0; i < 3; i++)
            equipped[i] = _equip;
    }
    public void UnEquipeNecro(RuneScript _unEquip)
    {
        //if(equipped == _unEquip)
        // equipped = null;
        
        //waiting for Equipped to be turned into array

        for (int i = 0; i < 3; i++)
        {
            if (equipped[i] == _unEquip)
                equipped[i] = null;
        }
    }
   public  void AddRune(RuneScript _rune)
    {
       int ID =_rune.GetID();
       runes[ID] = _rune;
    }
    public int GetNumBomb()
    {
        return numBombs;
    }
    public void AddBomb()
    {
        numBombs++;
    }
    public int GetNumRevives()
    {
        return numRevives;
    }
    public void AddRevive()
    {
        numRevives++;
    }
}
