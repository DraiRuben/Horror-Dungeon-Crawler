using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings_Menu : MonoBehaviour
{

    [SerializeField] private AudioMixer m_mixer;
    [SerializeField] private Slider m_slider;
    
    void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMasterVolume();
        }
    }


    public void SetMasterVolume()
    {
        float volume = m_slider.value;
        m_mixer.SetFloat("master",Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    private void LoadVolume()
    {
        m_slider.value = PlayerPrefs.GetFloat("masterVolume");
        SetMasterVolume();
    }
}
