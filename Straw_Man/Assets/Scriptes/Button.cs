using UnityEngine;
using UnityEngine.UI;
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
        //these prevent the player from being destroyed before entering key scenes
        if (_scene != 12 && _scene != 3 && _scene != 16 && _scene != 9 && _scene != 2 && _scene != 10)
        {
            Destroy(m_player);
        }

        if (_scene == 12)
        {
            sound.GetComponent<LoadSoundFX>().m_soundFXsources["MenuBGM"].Stop();
        }

        // LoadingScreen.show();
        AudioSource.PlayClipAtPoint(m_buttonClick, Camera.main.transform.position);

        //sound.GetComponent<LoadSoundFX>().m_soundFXsources["MenusSelect"].Play();
        Application.LoadLevel(_scene);
    }

    public void ExitGame()
    {
        print("TEST2");
        //AudioSource.PlayClipAtPoint(m_buttonSwoosh, Camera.main.transform.position);
        sound.GetComponent<LoadSoundFX>().m_soundFXsources["MenuMove"].Play();

        //while (sound.GetComponent<LoadSoundFX>().m_soundFXsources["MenuMove"].isPlaying) { }

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

    public void AdjustSFX(string _tag)
    {
        sound.GetComponent<LoadSoundFX>().m_soundFXVoume = GetComponent<Slider>().value;


        sound.GetComponent<LoadSoundFX>().m_soundFXsources["BombPickup"].Play();
    }

    public void AdjustMusic(string _tag)
    {
        sound.GetComponent<LoadSoundFX>().m_soundFXVoume = GetComponent<Slider>().value;
    }
}
