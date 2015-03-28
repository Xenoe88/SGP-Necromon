using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
    public AudioClip m_buttonClick, m_buttonSwoosh;
    
    public void ChangeScene(int _scene) 
    {
        AudioSource.PlayClipAtPoint(m_buttonClick, Camera.main.transform.position);

        Application.LoadLevel(_scene);
    }

    public void ExitGame()
    {
        AudioSource.PlayClipAtPoint(m_buttonSwoosh, Camera.main.transform.position);
        //only works during runtime, will not quit in Unity Editor
        Application.Quit();

    }

    public void ToggleFullScreen()
    {
        //only works during runtime, will not quit in Unity Editor        
        Screen.fullScreen = !Screen.fullScreen;
        AudioSource.PlayClipAtPoint(m_buttonSwoosh, Camera.main.transform.position);
    }
}
