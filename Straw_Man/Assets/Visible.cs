using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Visible : MonoBehaviour {

    //public GameObject[] m_this = new GameObject[10];
    public GameObject wolf;
    public List<GameObject> m_objects;
    public List<Transform> m_objectPos;

	// Use this for initialization
	void Start () 
    {
        //m_this[0] = wolf;
	}
	
	// Update is called once per frame
	void Update () 
    {
        //for (int i = 0; m_this[i]; i++)
        //{
        //    if (m_this[i] != null)
        //    {
        //        m_objects.Add(m_this[i]);
        //        m_objectPos.Add(m_this[i].transform);
        //    }

        m_objects.Add(wolf);
        m_objectPos.Add(wolf.transform);
        for (int i = 0; i < m_objects.Count; i++)
        {
            if (m_objects[i].renderer.isVisible)
            {
                Instantiate(m_objects[i], m_objectPos[i].position, m_objectPos[i].rotation);
            }
            else if (!m_objects[i].renderer.isVisible)
            {
                Destroy(m_objects[i]);
            }
        }
	}
}
