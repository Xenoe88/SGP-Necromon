using UnityEngine;
using System.Collections;

public class RuneScript : MonoBehaviour 
{
    GameObject necro;
    public int enemyID;
    public float cooldown;
    public bool isActive = false;
    public bool collected = false;

    public string descript;
    public  string NecroName;
    public string type;


	// Use this for initialization
	void Start () 
    {
        
	}


	// Update is called once per frame
	void Update () 
    {
        if (isActive)
        {
            cooldown -= Time.deltaTime;

            if (cooldown < 0)
                cooldown = 0;
        }
	}
  
    public void Summon()
    {
        if (cooldown <= 0)
        {
            Instantiate(necro, Camera.main.transform.position, Camera.main.transform.rotation);
            isActive = true;
        }
    }
    public int GetID() { return enemyID; }
    void OnTriggerEnter2D(Collider2D _target)
    {
        
        if(_target.tag == "Player")
        {
            collected = true;

            GetComponent<PlayerInventory>().AddRune(this);


            Destroy(this);     
        }
        
    }

}
