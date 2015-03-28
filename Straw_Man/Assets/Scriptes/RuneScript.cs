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
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public int GetID() { return enemyID; }

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
