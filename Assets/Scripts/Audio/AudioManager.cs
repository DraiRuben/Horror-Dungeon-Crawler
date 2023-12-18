using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("------------AudioSource------------")]

    [SerializeField] AudioSource m_musicSource;
    [SerializeField] AudioSource m_SFXSource;

    [Header("------------AudioClip------------")]

    public AudioClip background;

    public void Start()
    {
        m_musicSource.clip = background;
        m_musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        m_SFXSource.PlayOneShot(clip);
    }
}
