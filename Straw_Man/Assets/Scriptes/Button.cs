using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
    public AudioClip m_buttonClick, m_buttonSwoosh;
    public GameObject sound;
    public Camera up;
    public GameObject m_player;
    public Color highlighted;
    public Color pushed;

    private Color originalColor;
    
    public int level;
    public void Start()
    {
        sound = GameObject.FindGameObjectWithTag("MusicController");
        m_player = GameObject.FindGameObjectWithTag("Player");

        up = Camera.FindObjectOfType<Camera>();
        
       // originalColor = gameObject.renderer.material.color ;

    }

    public void ChangeScene(int _scene)
    {
        // LoadingScreen.show();
        AudioSource.PlayClipAtPoint(m_buttonClick, Camera.main.transform.position);

        //sound.GetComponent<LoadSoundFX>().m_soundFXsources["MenusSelect"].Play();
        Application.LoadLevel(_scene);
    }

    public void ExitGame()
    {
        print("TEST2");
        AudioSource.PlayClipAtPoint(m_buttonSwoosh, Camera.main.transform.position);
        sound.GetComponent<LoadSoundFX>().m_soundFXsources["MenusMove"].Play();

        //only works during runtime, will not quit in Unity Editor
        Application.Quit();

    }

    public void ResumeGame()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().SendMessage("EnterExitMenu", SendMessageOptions.DontRequireReceiver);
        Application.LoadLevel(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().m_lastLevel);
    }

    public void PlayerMenu()
    {
        //up.GetComponent<PauseMenuScript>().UnPauseGame();
        print("TEST1");
        Time.timeScale = 1;
        m_player.gameObject.GetComponent<PlayerController>().m_reLoadPosition = m_player.gameObject.transform.position;
        //ChangeScene(7);
        Application.LoadLevel("PlayerMenuScene");
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
    public void OnMouseDown()
    {
        gameObject.renderer.material.color = pushed;
    }

    public void OnMouseUpASButton()
    {
        if (level > -1)
            Application.LoadLevel(level);
        else 
            Application.Quit();
    }

    public void OnMouseEnter()
    {
        gameObject.renderer.material.color = highlighted;
    }
    public void OnMouseExit()
    {
        gameObject.renderer.material.color = originalColor;
    }
}
