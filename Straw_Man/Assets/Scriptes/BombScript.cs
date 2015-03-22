using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour 
{
    private bool test;

	// Use this for initialization
	void Start () 
    {
        rigidbody2D.AddForce(new Vector2(100 , 100));
        test = true;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (test)
        {
            Destroy(gameObject, 1.0f);
            test = false;
        }
	}
}
