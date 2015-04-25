using UnityEngine;
using System.Collections;

public class ReviveScript : MonoBehaviour 
{
    //public GameObject m_player;
    public AudioClip m_pickupSFX;
    public GameObject sound;
	// Use this for initialization
	void Start () 
    {
        sound = GameObject.FindGameObjectWithTag("MusicController");
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter2D(Collider2D _target)
    {
        if (_target.tag == "Player")
        {
            _target.GetComponent<PlayerInventory>().AddRevive();
            sound.GetComponent<LoadSoundFX>().m_soundFXsources["RevivePickup"].Play();
            Destroy(gameObject);
        }
    }
}
