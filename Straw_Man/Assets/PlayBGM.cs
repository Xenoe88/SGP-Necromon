using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayBGM : MonoBehaviour
{
    public GameObject m_player, music = null;
    public string m_level;

    // Use this for initialization
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        music = GameObject.FindGameObjectWithTag("MusicController");

        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (music == null)
        {
        music = GameObject.FindGameObjectWithTag("MusicController");
        }

        if (m_player == null)
        {
            m_player = GameObject.FindGameObjectWithTag("Player");
            m_level = "null";
        }

        if (m_player != null)
        {
            m_level = m_player.GetComponent<PlayerController>().m_lastLevel;
        }

        PlayMusic(m_level);
    }

    void PlayMusic(string _level)
    {
        switch (_level)
        {

            case "CourtyardScene":
                if (music.GetComponent<LoadSoundFX>().m_soundFXsources["HUBBGM"].isPlaying == true)
                    music.GetComponent<LoadSoundFX>().m_soundFXsources["HUBBGM"].Stop();

                if (music.GetComponent<LoadSoundFX>().m_soundFXsources["CourtyardBGM"].isPlaying == false)
                    music.GetComponent<LoadSoundFX>().m_soundFXsources["CourtyardBGM"].Play();

                break;
            case "tutorialScene":

                if (music.GetComponent<LoadSoundFX>().m_soundFXsources["CourtyardBGM"].isPlaying == false)
                    music.GetComponent<LoadSoundFX>().m_soundFXsources["CourtyardBGM"].Play();

                break;
            case "TowerScene":
                if (music.GetComponent<LoadSoundFX>().m_soundFXsources["HUBBGM"].isPlaying == true)
                    music.GetComponent<LoadSoundFX>().m_soundFXsources["HUBBGM"].Stop();

                if (music.GetComponent<LoadSoundFX>().m_soundFXsources["TowerBGM"].isPlaying == false)
                    music.GetComponent<LoadSoundFX>().m_soundFXsources["TowerBGM"].Play();

                break;
            case "DungeonScene":
                if (music.GetComponent<LoadSoundFX>().m_soundFXsources["HUBBGM"].isPlaying == true)
                    music.GetComponent<LoadSoundFX>().m_soundFXsources["HUBBGM"].Stop();

                if (music.GetComponent<LoadSoundFX>().m_soundFXsources["DungeonBGM"].isPlaying == false)
                    music.GetComponent<LoadSoundFX>().m_soundFXsources["DungeonBGM"].Play();

                break;
            case "EvilWizardScene":
                if (music.GetComponent<LoadSoundFX>().m_soundFXsources["HUBBGM"].isPlaying == true)
                    music.GetComponent<LoadSoundFX>().m_soundFXsources["HUBBGM"].Stop();

                if (music.GetComponent<LoadSoundFX>().m_soundFXsources["BossBGM"].isPlaying == false)
                    music.GetComponent<LoadSoundFX>().m_soundFXsources["BossBGM"].Play();

                break;
            case "ThroneRoomeScene":

                if (music.GetComponent<LoadSoundFX>().m_soundFXsources["MenuBGM"].isPlaying == true)
                    music.GetComponent<LoadSoundFX>().m_soundFXsources["MenuBGM"].Stop();

                if (music.GetComponent<LoadSoundFX>().m_soundFXsources["HUBBGM"].isPlaying == false)
                    music.GetComponent<LoadSoundFX>().m_soundFXsources["HUBBGM"].Play();

                break;
            default:

                if (music.GetComponent<LoadSoundFX>().m_soundFXsources["MenuBGM"].isPlaying == false)
                    music.GetComponent<LoadSoundFX>().m_soundFXsources["MenuBGM"].Play();

                break;
        }

    }

    public void AdjustSFX()
    {
        music = GameObject.FindGameObjectWithTag("MusicController");

        GameObject temp = GameObject.FindGameObjectWithTag("SFX");
        float oldVolume = music.GetComponent<LoadSoundFX>().m_soundFXVoume;
        float newVolume = temp.GetComponent<Slider>().value;


        music.GetComponent<LoadSoundFX>().m_soundFXVoume = temp.GetComponent<Slider>().value;

        if (oldVolume != newVolume)
            music.GetComponent<LoadSoundFX>().m_soundFXsources["MenuSelect"].Play(); 

    }
    
    public void AdjustMusic()
    {
        music = GameObject.FindGameObjectWithTag("MusicController");

        GameObject temp = GameObject.FindGameObjectWithTag("Music");
        float oldVolume = music.GetComponent<LoadSoundFX>().m_backgroundMusicVolume;
        float newVolume = temp.GetComponent<Slider>().value;

        music.GetComponent<LoadSoundFX>().m_backgroundMusicVolume = temp.GetComponent<Slider>().value;
    }
}
