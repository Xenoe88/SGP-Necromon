using UnityEngine;
using System.Collections;

public class ExplodingBox : MonoBehaviour
{
    void Update()
    {
        Destroy(this.gameObject, 0.5f);
    }
    void OnTriggerEnter2D(Collider2D _target)
    {
        _target.SendMessage("ModifyHealth", -50,SendMessageOptions.DontRequireReceiver);
        _target.SendMessage("Explode", SendMessageOptions.DontRequireReceiver);
    }
}
