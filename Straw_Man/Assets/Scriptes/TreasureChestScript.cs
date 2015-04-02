using UnityEngine;
using System.Collections;

public class TreasureChestScript : MonoBehaviour {

    public Animator m_animate;

    public GameObject m_object;

    public AudioClip m_sfx;

	// Use this for initialization
	void Start () {
        m_animate = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
        AudioSource.PlayClipAtPoint(m_sfx, transform.position);
        m_animate.SetInteger("AnimState", 1);
        }
    }

    void InstantiateandDelete()
    {
        Destroy(this.gameObject, 1.0f);
        if (m_object)
        {
            GameObject clone = Instantiate(m_object, transform.position, Quaternion.identity) as GameObject;
        }
    }
}
