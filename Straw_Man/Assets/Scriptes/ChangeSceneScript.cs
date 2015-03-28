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

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !LoadLock)
            LoadScene();
	}
    void LoadScene()
    {
        Application.LoadLevel(sceneNum);
    }
}
