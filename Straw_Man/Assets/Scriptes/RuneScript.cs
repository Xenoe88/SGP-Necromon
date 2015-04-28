using UnityEngine;
using System.Collections;

public class RuneScript : MonoBehaviour
{
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
        if (_target.tag == "Player")
        {
            _target.GetComponent<PlayerInventory>().AddRune(enemyID);

            //shouldn't set revive position inside the boss room
            if (_target.GetComponent<PlayerScript>().m_inBossRoom == false)
                _target.GetComponent<PlayerController>().SetRevivePosition(gameObject.transform.position);                

            animator.SetInteger("RuneAnim", 1);
        }
    }
    public void SetID(int _ID) { enemyID = _ID; }
    void Destroy() { Destroy(gameObject); }

}
