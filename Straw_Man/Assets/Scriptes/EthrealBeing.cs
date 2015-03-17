using UnityEngine;
using System.Collections;

public class EthrealBeing : MonoBehaviour {

    public int Health = 100;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ModifyHealth(int _dmg)
    {
        Health += _dmg;
    }
        void OnTriggerEnter2D(Collider2D _target)
    {
        _target.SendMessage("ModifyHealth", -5, SendMessageOptions.DontRequireReceiver);

    }
}
