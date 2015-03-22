using UnityEngine;
using System.Collections;

public class ReviveScript : MonoBehaviour 
{
    public Entity m_player;
    public AudioClip m_pickupSFX;

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
        if (_target.tag == "Player")
        {
            m_player.GetComponent<PlayerInventory>().AddRevive();
            AudioSource.PlayClipAtPoint(m_pickupSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
