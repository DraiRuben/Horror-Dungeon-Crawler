using UnityEngine;

public class AudioManagerSister : MonoBehaviour
{
    [Header("------------AudioSource------------")]

    [SerializeField] private AudioSource m_SFXSourceSister;

    [Header("------------AudioClip------------")]

    [Header("Boss")]

    public AudioClip Sister_Idle;
    public AudioClip Sister_Attack;
    public AudioClip Sister_Damaged;
    public AudioClip Sister_Death;


    public void PlaySFXSister(AudioClip clip)
    {
        m_SFXSourceSister.PlayOneShot(clip);
    }
}
