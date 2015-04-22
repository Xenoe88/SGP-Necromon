using UnityEngine;
using System.Collections;

public class LoseScenChange : MonoBehaviour {


    public string scene;
    public int sceneNum;
    private bool LoadLock;
    // Use this for initialization
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            LoadScene();
    }
    void LoadScene()
    {
        print("credits");
        Application.LoadLevel("CreditsScene");
    }
}
