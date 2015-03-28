using UnityEngine;
using System.Collections;

public class PatrolPointScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D _turn)
    {
        print(_turn.transform.localScale.x);
        _turn.transform.localScale = new Vector3((transform.localScale.x == 1) ? -1 : 1, 1, 1);


    }
}
