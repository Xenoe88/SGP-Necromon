using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetSliders : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
    {
        GameObject music = GameObject.FindGameObjectWithTag("MusicController");
        GameObject SFX = GameObject.FindGameObjectWithTag("SFX");

        SFX.GetComponent<Slider>().value = music.GetComponent<LoadSoundFX>().m_soundFXVoume;

        GameObject BGM = GameObject.FindGameObjectWithTag("Music");

        BGM.GetComponent<Slider>().value = music.GetComponent<LoadSoundFX>().m_backgroundMusicVolume;
	}
}
