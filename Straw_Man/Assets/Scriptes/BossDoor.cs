using UnityEngine;
using System.Collections;

public class BossDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D _target)
    {
        if (_target.gameObject.GetComponent<PlayerScript>().m_gameStatus == 3)
            Application.LoadLevel("EvilWizardScene");

            //GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<LoadingScreen>().ChangeLevel("EvilWizardScene");

    }
}
