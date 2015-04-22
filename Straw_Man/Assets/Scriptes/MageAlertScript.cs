using UnityEngine;
using System.Collections;

public class MageAlertScript : MonoBehaviour {

    public MageScript owner;
	void OnTriggerEnter2D(Collider2D collider)
    {
        owner.target = collider.gameObject;
    }
}
