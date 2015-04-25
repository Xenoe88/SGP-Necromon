using UnityEngine;
using System.Collections;

public class CourtyardScenePortal : MonoBehaviour 
{
    //public string level;

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
        Application.LoadLevel("CourtyardScene");
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        LoadScene();
        //GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<LoadingScreen>().ChangeLevel("CourtyardScene");
    }
}
