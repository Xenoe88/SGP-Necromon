using UnityEngine;
using System.Collections;

public class TowerLevelPortal : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    void LoadScene()
    {
        Application.LoadLevel(13);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.GetComponent<PlayerScript>().m_gameStatus > 1)
        {
            LoadScene();
        }
    }
}
