using UnityEngine;
using System.Collections;

public class AlertScript : MonoBehaviour 
{
    public Demon patrol;

    void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider.gameObject.tag != patrol.gameObject.tag )
            patrol.target = _collider.gameObject;

    }
}
