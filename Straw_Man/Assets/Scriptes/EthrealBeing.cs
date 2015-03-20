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


        void OnTriggerEnter2D(Collider2D _target)
    {
        _target.SendMessage("ModifyHealth", -5, SendMessageOptions.DontRequireReceiver);

    }
}
