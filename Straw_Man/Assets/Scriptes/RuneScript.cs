using UnityEngine;
using System.Collections;

public class RuneScript : MonoBehaviour 
{
    public float m_timerAmount;
    public GameObject necro, m_player;
    public int enemyID;
    public float cooldown;
    public bool isActive = false;
    public bool collected = false;

    public string descript;
    public  string NecroName;
    public string type;

    private Animator animator;

	// Use this for initialization
	void Start () 
    {
        animator = GetComponent<Animator>();
	}

	// Update is called once per frame
    //void Update () 
    //{
    //    if (isActive)
    //    {
    //        cooldown -= Time.deltaTime;

    //        if (cooldown < 0)
    //            cooldown = 0;
    //    }
    //}
  
    //public void Summon(Vector3 _position)
    //{
    //    if (isActive == false && cooldown <= 0)
    //    {
    //        GameObject temp = (GameObject)Instantiate(necro, _position, Camera.main.transform.rotation);
    //        temp.SendMessage("MakeNecro", SendMessageOptions.DontRequireReceiver);
    //        //Instantiate(m_necroBox, _position, Camera.main.transform.rotation);
    //        isActive = true;
    //        cooldown = m_timerAmount;
    //    }
    //}

    public int GetID() { return enemyID; }

    //public void EnemyInactive(int _necroID) 
    //{ 
    //    isActive = false;
    //}

    void OnTriggerEnter2D(Collider2D _target)
    {
        if(_target.tag == "Player")
        {
            //collected = true;
            necro.GetComponent<Entity>().Owner = _target.gameObject;
            necro.GetComponent<Entity>().Owner.GetComponent<PlayerInventory>().AddRune(enemyID);

            animator.SetInteger("RuneAnim", 1);
        }
    }
    public void SetID(int _ID)
    {
        print(_ID);
        enemyID = _ID;
    }
    void Destroy()
    {
        Destroy(gameObject);
    }

}
