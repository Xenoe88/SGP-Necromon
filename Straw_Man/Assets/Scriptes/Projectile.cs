using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    public int m_damage = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag != "Turret")
        {
            target.SendMessage("ModifyHealth", -m_damage, SendMessageOptions.DontRequireReceiver);
            Destroy(this.gameObject);
        }
    }
}
