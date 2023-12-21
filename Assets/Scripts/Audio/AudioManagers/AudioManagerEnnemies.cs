using UnityEngine;

public class AudioManagerEnnemies : MonoBehaviour
{

    [Header("------------AudioSource------------")]

    [SerializeField] private AudioSource m_SFXSourceMob;

    [Header("------------AudioClip------------")]

    [Header("Mobs")]

    public AudioClip Dog_Idle;
    public AudioClip Dog_Attack;
    public AudioClip Dog_Damaged;
    public AudioClip Dog_Death;


    public void PlaySFXMob(AudioClip clip)
    {
        m_SFXSourceMob.PlayOneShot(clip);
    }
}
