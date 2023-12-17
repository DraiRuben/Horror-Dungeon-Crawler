using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings_Menu : MonoBehaviour
{

    [SerializeField] private AudioMixer m_mixer;
    [SerializeField] private Slider m_masterVolumeSlider;
    [SerializeField] private Slider m_musicVolumeSlider;
    [SerializeField] private Slider m_SFXVolumeSlider;

    void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMasterVolume();
            SetMusicVolume();
            SetSFXVolume();
        }
        gameObject.SetActive(false);
    }


    public void SetMasterVolume()
    {
        float volume = m_masterVolumeSlider.value;
        m_mixer.SetFloat("master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    public void SetMusicVolume()
    {
        float volume = m_musicVolumeSlider.value;
        m_mixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = m_SFXVolumeSlider.value;
        m_mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolume()
    {
        m_masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        m_musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        m_SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }
}
