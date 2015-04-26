using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BombScript : MonoBehaviour 
{
    ArrayList list = new ArrayList();
    public GameObject m_area, m_player;
    bool thrown = false;
    public Animator animate;
    public GameObject sound;
	// Use this for initialization
	void Start () 
    {
        sound = GameObject.FindGameObjectWithTag("MusicController");

        animate = GetComponent<Animator>();

        //print(m_player.gameObject.GetComponent<PlayerController>().m_facingDirection);

    //rigidbody2D.AddForce(new Vector2(100 * m_player.gameObject.GetComponent<PlayerController>().m_facingDirection, 50));

	}
	
	// Update is called once per frame
	void Update ()
    {

        if (thrown)
        {
            sound.GetComponent<LoadSoundFX>().m_soundFXsources["BombFuse"].Play();
            animate.SetInteger("AnimState", 1);
        }
	
	}
    void OntriggerEnter2D(Collider2D _target)
    {
        if (list.Contains(_target.gameObject))
            return;
        list.Add(_target);

    }

    void OntriggerExit2D(Collider _leave)
    {
        if (list.Contains(_leave.gameObject))
            list.Remove(_leave.gameObject);

    }
    public  void Explode()
    {
        //modified instantite position so the bomb destroys the tiles it's sitting on
        GameObject temp = (GameObject)Instantiate(m_area, transform.position - new Vector3(0.0f, 0.5f, 0.0f), transform.rotation);
        sound.GetComponent<LoadSoundFX>().m_soundFXsources["BombExplode"].Play();

        Destroy(temp, 0.2f);
        Destroy(gameObject);
    }
}

