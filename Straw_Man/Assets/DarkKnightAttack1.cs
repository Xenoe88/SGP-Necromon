using UnityEngine;
using System.Collections;

public class DarkKnightAttack1 : MonoBehaviour
{
    public GameObject m_hitBox, m_owner;


	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void Attack()
    {
        GameObject temp = (GameObject)Instantiate(m_hitBox, transform.position, transform.rotation);
        temp.SendMessage("SetPlayer", m_owner, SendMessageOptions.RequireReceiver);
        Destroy(temp, 2.0f);
    }

    void SetOwner(/*GameObject _target,*/ GameObject _owner) 
    { 
        //m_target = _target;
        m_owner = _owner;
    }

    void Destroy() { Destroy(gameObject); }
}
