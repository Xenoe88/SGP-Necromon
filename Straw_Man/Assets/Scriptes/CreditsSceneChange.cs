using UnityEngine;
using System.Collections;

public class CreditsSceneChange : MonoBehaviour {

    private bool LoadLock;

    private GameObject m_player;

	// Use this for initialization
	void Start () {
        m_player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return) && !LoadLock)
            LoadScene();
	}
    void LoadScene()
    {

        if (m_player.GetComponent<PlayerController>().m_lastLevel == "LoseScene" || m_player.GetComponent<PlayerController>().m_lastLevel == "WinScene")
        {
            Destroy(m_player);
            Destroy(GameObject.FindGameObjectWithTag("MusicController"));

            Application.LoadLevel(0);
            return;
        }

        Destroy(m_player);
        Application.LoadLevel(1);
    }
}
