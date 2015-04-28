using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour 
{
    public string m_loadLevel;

    public GameObject m_loadScreenText;

	// Use this for initialization
	void Start () 
    {
        m_loadScreenText.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    public void ChangeLevel(string _level)
    {
        Application.LoadLevelAsync(_level);
        StartCoroutine(DisplayLoadingScreen(_level));
    }

    IEnumerator DisplayLoadingScreen(string _level)
    {
        m_loadScreenText.SetActive(true);

        AsyncOperation async = Application.LoadLevelAsync(_level);

        yield return null;
    }
}
