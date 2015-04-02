using UnityEngine;
using System.Collections;

public class TreasureChestScript : MonoBehaviour {

    public Animator m_animate;

    public GameObject[] m_object;

    public AudioClip m_sfx;

    public bool m_opened = false;

	// Use this for initialization
	void Start () 
    {
        m_animate = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player" && !m_opened)
        {
        AudioSource.PlayClipAtPoint(m_sfx, transform.position);
        m_animate.SetInteger("AnimState", 1);
        m_opened = true;
        }
    }

    void InstantiateandDelete()
    {
        Destroy(this.gameObject, 1.5f);
        int randomObject = Random.Range(1, 100);
        if (randomObject > 1 && randomObject < 30)
        {
            int randomGood = Random.Range(0, 2);
            GameObject clone = Instantiate(m_object[randomGood], transform.position, Quaternion.identity) as GameObject;
        }
        else if (randomObject > 30 && randomObject < 100)
        {
            int randomEnemy = Random.Range(1, 6);
            GameObject clone = Instantiate(m_object[randomEnemy], transform.position, Quaternion.identity) as GameObject;
        }
    }
}
