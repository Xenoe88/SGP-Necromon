using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdjustBGM : MonoBehaviour
{

    public Slider m_slider;
    public GameObject music;

    // Use this for initialization
    void Start()
    {
        music = GameObject.FindGameObjectWithTag("MusicController");

        m_slider.value = music.GetComponent<LoadSoundFX>().m_backgroundMusicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            music.GetComponent<LoadSoundFX>().m_backgroundMusicVolume = m_slider.value;

            //PlayMusicSFX(m_slider.tag);
        }
    }

    void PlayMusicSFX(string _tag)
    {
        //music.GetComponent<LoadSoundFX>().m_soundFXsources["BombPickup"].Play();
    }
}
