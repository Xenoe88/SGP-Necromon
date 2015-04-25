using UnityEngine;
using System.Collections;

public class BombPickupScript : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("MusicController").GetComponent<LoadSoundFX>().m_soundFXsources["BombPickup"].Play();
            _collider.gameObject.GetComponent<PlayerInventory>().AddBomb();
            Destroy(gameObject);
        }
    }

}
