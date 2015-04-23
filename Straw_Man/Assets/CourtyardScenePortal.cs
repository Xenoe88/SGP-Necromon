using UnityEngine;
using System.Collections;

public class CourtyardScenePortal : MonoBehaviour {

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
        Application.LoadLevel(4);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        LoadScene();
    }
}
