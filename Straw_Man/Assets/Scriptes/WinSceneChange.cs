using UnityEngine;
using System.Collections;

public class WinSceneChange : MonoBehaviour 
{
    public string scene;
    public int sceneNum;
    private bool LoadLock = false;

	// Use this for initialization
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !LoadLock)
            LoadScene();
    }
    void LoadScene()
    {
        Application.LoadLevel(9);
    }
}
