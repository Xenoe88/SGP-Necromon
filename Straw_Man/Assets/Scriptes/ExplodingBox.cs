using UnityEngine;
using System.Collections;

public class ExplodingBox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D _target)
    {
        _target.SendMessage("ModifyHealth", -50,SendMessageOptions.DontRequireReceiver);
    }
}
