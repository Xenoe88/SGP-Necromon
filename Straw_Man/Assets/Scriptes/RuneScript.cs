using UnityEngine;
using System.Collections;

public class RuneScript : MonoBehaviour 
{
    public float m_timerAmount;
    //public GameObject necro, m_player;
    public int enemyID;
    public float cooldown;
    //public bool isActive = false;
    //public bool collected = false;

    //public string descript;
    //public  string NecroName;
    //public string type;

    private Animator animator;

	// Use this for initialization
	void Start () 
    {
        animator = GetComponent<Animator>();
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
            print("RUNEONTRIGGER");
            _target.GetComponent<PlayerInventory>().AddRune(enemyID);

            animator.SetInteger("RuneAnim", 1);
        }
    }

    void Destroy() { Destroy(gameObject); }

}
