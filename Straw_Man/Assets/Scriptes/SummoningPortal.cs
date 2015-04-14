using UnityEngine;
using System.Collections;

public class SummoningPortal : MonoBehaviour {

    public GameObject[] m_summoners;

	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () {
	}

    void SummonEnemy()
    {
        int randRange = Random.Range(0, 3);
        GameObject clone = Instantiate(m_summoners[randRange], transform.position, Quaternion.identity) as GameObject;
        Destroy(this.gameObject);
    }
}
