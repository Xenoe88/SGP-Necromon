using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VolumeControl : MonoBehaviour
{
    public Slider m_slider;
    public AudioClip m_sound;
    public GameObject music;
   // public float prevVol, newVol;

    // Use this for initialization
    void Start()
    {
        music = GameObject.FindGameObjectWithTag("MusicController");

        m_slider = this.GetComponent<Slider>();

        if (m_slider.tag == "Music")
        {
            //print("music");
            m_slider.value = music.GetComponent<LoadSoundFX>().m_backgroundMusicVolume;
            //m_slider.value = AudioListener.volume;
        }
        else if (m_slider.tag == "SFX")
        {
            //print("sfx");
            m_slider.value = music.GetComponent<LoadSoundFX>().m_soundFXVoume;
            //m_slider.value = AudioListener.volume;
        }
    }

    // Update is called once per frame
    void Update()
    {
       // prevVol = AudioListener.volume;
        //AudioListener.volume = m_slider.value;
        //newVol = AudioListener.volume;

        if (m_slider.tag == "Music" && Input.GetKeyUp(KeyCode.Mouse0))
        {
            print("music");
            music.GetComponent<LoadSoundFX>().m_backgroundMusicVolume = m_slider.value;
            //m_slider.value = AudioListener.volume;

            //if (Input.GetKeyUp(KeyCode.Mouse0))
                PlayMusicSFX(m_slider.tag);
        }
        else if (m_slider.tag == "SFX" && Input.GetKeyUp(KeyCode.Mouse0))
        {
            print("sfx");
            music.GetComponent<LoadSoundFX>().m_soundFXVoume = m_slider.value;
            //m_slider.value = AudioListener.volume;

            //if (Input.GetKeyUp(KeyCode.Mouse0))
                PlayMusicSFX(m_slider.tag);
        }



    }

    void OnMouseUp()
    {
        //AudioSource.PlayClipAtPoint(m_sound, Camera.main.transform.position);
    }

    void PlayMusicSFX(string _tag)
    {
        print(_tag);

        if (_tag == "Music")
        {
            music.GetComponent<LoadSoundFX>().m_soundFXsources["CourtyardBGM"].Play();            
        }
        else if (_tag == "SFX")
        {
            music.GetComponent<LoadSoundFX>().m_soundFXsources["BombPickup"].Play();            
        }
    }
}
