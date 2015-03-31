using UnityEngine;
using System.Collections;

public class RuneScript : MonoBehaviour 
{
    public GameObject necro;
    public int enemyID;

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
