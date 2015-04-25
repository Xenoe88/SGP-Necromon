using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadSoundFX : MonoBehaviour 
{
    public Dictionary<string, AudioSource> m_soundFXsources;
    public AudioSource[] sources;
    public float m_backgroundMusicVolume, m_soundFXVoume;

	// Use this for initialization
	void Start () 
    {
        m_backgroundMusicVolume = 100;
        m_soundFXVoume = 100;

        if (m_soundFXsources == null)
        {
            m_soundFXsources = new Dictionary<string,AudioSource>();

            sources = gameObject.GetComponentsInChildren<AudioSource>();

            for (int i = 0; i < sources.Length; i++)
                m_soundFXsources[sources[i].gameObject.name] = sources[i];
        }

        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () 
    {

        for (int i = 0; i < sources.Length; i++)
        {
            switch (sources[i].gameObject.name)
            {
                case "CourtyardBGM":
                    m_soundFXsources[sources[i].gameObject.name].volume = m_backgroundMusicVolume;
                    break;
                case "DungeonBGM":
                    m_soundFXsources[sources[i].gameObject.name].volume = m_backgroundMusicVolume;
                    break;
                case "TowerBGM":
                    m_soundFXsources[sources[i].gameObject.name].volume = m_backgroundMusicVolume;
                    break;
                case "BossBGM":
                    m_soundFXsources[sources[i].gameObject.name].volume = m_backgroundMusicVolume;
                    break;
                case "HUBBGM":
                    m_soundFXsources[sources[i].gameObject.name].volume = m_backgroundMusicVolume;
                    break;
                case "MenuBGM":
                    m_soundFXsources[sources[i].gameObject.name].volume = m_backgroundMusicVolume;
                    break;
                default:
                    m_soundFXsources[sources[i].gameObject.name].volume = m_soundFXVoume;
                    break;
            }
        }
	}
}
