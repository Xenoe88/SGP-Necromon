using UnityEngine;
using System.Collections;

public class AlertScript : MonoBehaviour 
{
    public Demon patrol;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider.gameObject.tag != this.tag )
            patrol.m_target = _collider.gameObject;
    }
}
