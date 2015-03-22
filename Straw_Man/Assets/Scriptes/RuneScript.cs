using UnityEngine;
using System.Collections;

public class RuneScript : MonoBehaviour 
{
    public float m_timerAmount;
    public GameObject necro;
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
  
    public void Summon(Vector3 _position, float _cooldown)
    {
        if (isActive == false && cooldown <= 0)
        {
            Instantiate(necro, _position, Camera.main.transform.rotation);
            isActive = true;
            cooldown = _cooldown;
        }
    }

    public int GetID() { return enemyID; }

    public void EnemyInactive() { isActive = false; }

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
