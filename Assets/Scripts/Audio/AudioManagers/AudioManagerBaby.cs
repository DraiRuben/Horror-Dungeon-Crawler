using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerBaby : MonoBehaviour
{
    [Header("------------AudioSource------------")]

    [SerializeField] AudioSource m_SFXSourceBaby;

    [Header("------------AudioClip------------")]

    [Header("Boss")]

    public AudioClip Baby_Phase1_Idle;
    public AudioClip Baby_Phase2_Idle;
    public AudioClip Baby_Attack;
    public AudioClip Baby_Damaged;
    public AudioClip Baby_Death;


    public void PlaySFXBaby(AudioClip clip)
    {
        m_SFXSourceBaby.PlayOneShot(clip);
    }
}
