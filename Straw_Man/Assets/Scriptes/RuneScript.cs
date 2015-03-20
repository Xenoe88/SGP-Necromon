using UnityEngine;
using System.Collections;

public class RuneScript : MonoBehaviour 
{
    public int enemyID;
    float cooldown;
    bool isActive = false;
    bool collected = false;

    string descript;
    string NecroName;
    string type;


	// Use this for initialization
	void Start () 
    {
        
	}


	// Update is called once per frame
	void Update () 
    {
	    
	}
    public int GetID() { return enemyID; }
    void OnTriggerEnter2D(Collider2D _target)
    {
        
        if(_target.tag == "Player")
        {
            GetComponent<PlayerInventory>().AddRune(this);

            collected = true;

            //Destroy(this);     
        }
        
    }

}
