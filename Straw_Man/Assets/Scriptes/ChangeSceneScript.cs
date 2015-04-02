using UnityEngine;
using System.Collections;

public class ChangeSceneScript : MonoBehaviour
{
    public string scene;
    public int sceneNum;
    private bool LoadLock;
    

    // Use this for initialization
    void Start()
    {
        LoadLock = false;
        //Screen.SetResolution(800, 600, true);
        //Application.targetFrameRate = 30;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !LoadLock)
            LoadScene();
	}
    void LoadScene()
    {
        Application.LoadLevel(1);
    }
}
