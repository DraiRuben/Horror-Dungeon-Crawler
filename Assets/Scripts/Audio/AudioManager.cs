using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("------------AudioSource------------")]

    [SerializeField] AudioSource m_musicSource;
    [SerializeField] AudioSource m_SFXSource;

    [Header("------------AudioClip------------")]

    public AudioClip footstep_Concrete;

    [Header("Items")]

    public AudioClip pickup_Items;
    public AudioClip key;
    public AudioClip bandage;
    public AudioClip pills;
    public AudioClip matches;
    public AudioClip gun;
    public AudioClip punch;
    public AudioClip knife;
    public AudioClip magic_Arms;

    [Header("Mobs")]

    public AudioClip dog_Idle;
    public AudioClip dog_Attack;
    public AudioClip dog_Damaged;
    public AudioClip dog_Death;

    [Header("Boss")]

    public AudioClip father_Phase1_Idle;
    public AudioClip father_Phase2_Idle;
    public AudioClip father_Attack;
    public AudioClip father_Damaged;
    public AudioClip father_Death;

    [Header("------------Music------------")]

    public AudioClip main_Menu;
    public AudioClip victory_Menu;
    public AudioClip defeat_Menu;
    public AudioClip Basement;

    public void PlaySFX(AudioClip clip)
    {
        m_SFXSource.PlayOneShot(clip);
    }
    public void PlayMusic(AudioClip music)
    {
        m_musicSource.PlayOneShot(music);
        m_musicSource.loop = true;
    }
}
