using UnityEngine;
using System.Collections;

public class Demon : MonoBehaviour 
{

    public bool m_isNecro = false;
    public RuneScript m_rune;
    public int slot = 7;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Destroy()
    {
        int randomVariable = Random.Range(0, 100);

        //TODO - also need to test if we've already collected this enemies rune
        if (randomVariable >= 0 && randomVariable <= 20 && m_isNecro == false)
        {
            GameObject temp = (GameObject)Instantiate(m_rune, transform.position, transform.rotation);
            temp.SendMessage("SetID", slot, SendMessageOptions.DontRequireReceiver);
        }

        if (m_isNecro)
        {
            GetComponent<PlayerInventory>().SendMessage("EnemyActive", slot, SendMessageOptions.DontRequireReceiver);
        }

        Destroy(gameObject);
    }
}
