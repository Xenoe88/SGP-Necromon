using UnityEngine;
using System.Collections;

public class InstructionsChangeScene : MonoBehaviour {

    private bool LoadLock;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !LoadLock)
            LoadScene();
    }
    void LoadScene()
    {
        Application.LoadLevel(1);
    }
}
