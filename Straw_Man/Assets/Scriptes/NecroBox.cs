using UnityEngine;
using System.Collections;

public class NecroBox : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter2D(Collider2D _target)
    {
        if (_target.tag == "Enemy")
            _target.SendMessage("MakeNecro", SendMessageOptions.DontRequireReceiver);

        //Destroy(gameObject);
    }
}
