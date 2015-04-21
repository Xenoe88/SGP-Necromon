using UnityEngine;
using System.Collections;

public class LightningAttack : MonoBehaviour {

    public GameObject m_target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Attack()
    {
        if (m_target)
        {
            m_target.SendMessage("ModifyHealth", -20, SendMessageOptions.DontRequireReceiver);
        }
        Destroy(this.gameObject);
    }

    void OnTriggerStay2D(Collider2D target)
    {
        m_target = target.gameObject;
    }

    void OnTriggerExit2D(Collider2D target)
    {
        m_target = null;
    }
}
