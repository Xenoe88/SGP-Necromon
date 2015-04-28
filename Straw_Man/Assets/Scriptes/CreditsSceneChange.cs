using UnityEngine;
using System.Collections;

public class CreditsSceneChange : MonoBehaviour {
    
    private GameObject m_player;

	// Use this for initialization
	void Start () {
        m_player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
            LoadScene();
	}
    void LoadScene()
    {
        if (m_player.GetComponent<PlayerController>().m_lastLevel == "LoseScene" || m_player.GetComponent<PlayerController>().m_lastLevel == "WinScene")
        {
            Destroy(m_player);
            GameObject temp = GameObject.FindGameObjectWithTag("MusicController");
            Destroy(temp);

            Application.LoadLevel("TitleScene");
            return;
        }

        Destroy(m_player);
        Application.LoadLevel(1);
    }
}
