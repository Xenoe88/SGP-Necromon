using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour 
{
    ArrayList list = new ArrayList();
    public GameObject m_area;
	// Use this for initialization
	void Start () 
    {


	}
	
	// Update is called once per frame
	void Update ()
    {
	
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
        GameObject temp = (GameObject)Instantiate(m_area, transform.position, transform.rotation);

        Destroy(temp, 5.0f);
        Destroy(gameObject);
    }
}

