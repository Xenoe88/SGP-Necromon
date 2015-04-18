using UnityEngine;
using System.Collections;

public class ControlChangeScene : MonoBehaviour {

    private bool LoadLock;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !LoadLock)
            LoadScene();
           }
    void LoadScene()
    {
        Application.LoadLevel(3);
    }
}
