using UnityEngine;
using System.Collections;

public class BombPickupScript : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider.gameObject.tag == "Player")
        {
            _collider.gameObject.GetComponent<PlayerInventory>().AddBomb();
            Destroy(gameObject);
        }
    }

}
