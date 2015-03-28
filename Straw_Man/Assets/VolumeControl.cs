using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VolumeControl : MonoBehaviour
{
    public Slider m_slider;
    public AudioClip m_sound;
    public float prevVol, newVol;

    // Use this for initialization
    void Start()
    {
        m_slider = GetComponent<Slider>();

        if (m_slider.tag == "Music")
        {
            print("music");
            m_slider.value = AudioListener.volume;
        }
        else if (m_slider.tag == "SFX")
        {
            print("sfx");
            m_slider.value = AudioListener.volume;
        }
    }

    // Update is called once per frame
    void Update()
    {
        prevVol = AudioListener.volume;
        AudioListener.volume = m_slider.value;
        newVol = AudioListener.volume;

        if (Input.GetKeyUp(KeyCode.Mouse0))
            OnMouseUp();
    }

    void OnMouseUp()
    {
        AudioSource.PlayClipAtPoint(m_sound, Camera.main.transform.position);
    }
}
