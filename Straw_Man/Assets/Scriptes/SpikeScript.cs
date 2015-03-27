using UnityEngine;
using System.Collections;

public class SpikeScript : MonoBehaviour {

    public AudioClip m_sound;
	// Use this for initialization
    public int m_damage = 300;
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter2D(Collider2D target)
    {
        target.SendMessage("ModifyHealth", -m_damage, SendMessageOptions.DontRequireReceiver);
        AudioSource.PlayClipAtPoint(m_sound, transform.position);
    }
}
