using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
    public AudioClip m_buttonClick, m_buttonSwoosh;
    public Camera up;
    public GameObject m_player;

    public void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");

        up = Camera.FindObjectOfType<Camera>();
      
    }

    public void ChangeScene(int _scene) 
    {

        AudioSource.PlayClipAtPoint(m_buttonClick, Camera.main.transform.position);

        Application.LoadLevel(_scene);
    }

    public void ExitGame()
    {
        print("TEST2");
        AudioSource.PlayClipAtPoint(m_buttonSwoosh, Camera.main.transform.position);
        //only works during runtime, will not quit in Unity Editor
        Application.Quit();

    }

    public void PlayerMenu()
    {
        //up.GetComponent<PauseMenuScript>().UnPauseGame();
        print("TEST1");
        Time.timeScale = 1;
        m_player.gameObject.GetComponent<PlayerController>().m_reLoadPosition = m_player.gameObject.transform.position;
        ChangeScene(7);
    }

    public void ToggleFullScreen()
    {
        //only works during runtime, will not quit in Unity Editor        
        Screen.fullScreen = !Screen.fullScreen;
        AudioSource.PlayClipAtPoint(m_buttonSwoosh, Camera.main.transform.position);
    }

    public void TogglePauseMenu()
    {
       up.GetComponent<PauseMenuScript>().UnPauseGame();
    }
}
