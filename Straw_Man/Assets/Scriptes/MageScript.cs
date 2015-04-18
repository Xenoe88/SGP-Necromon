using UnityEngine;
using System.Collections;

public class MageScript : MonoBehaviour
{

    public GameObject target;
    public AudioClip sounds;
    private Entity mage;

    public bool attack;

	// Use this for initialization
	void Start ()
    {
        mage = GetComponent<Entity>();
        mage.m_animator = GetComponent<Animator>();
        mage.m_speed = 2;
        mage.m_health = 300;

	}
	
	// Update is called once per frame
	void Update () 
    {
        if(mage.m_health <= 0)
        {
            Die();
            return;
        }
        if(target)
        {
            if (target.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (target.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

        }
	
	}

    void Die()
    {
        Destroy(this.gameObject);
        //target.SendMessage("ModifyStatus", SendMessageOptions.DontRequireReceiver);
        Application.LoadLevel(12); 
    }
}
