using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerFather : MonoBehaviour
{
    [Header("------------AudioSource------------")]

    [SerializeField] AudioSource m_SFXSourceFather;

    [Header("------------AudioClip------------")]

    [Header("Boss")]

    public AudioClip Father_Phase1_Idle;
    public AudioClip Father_Phase2_Idle;
    public AudioClip Father_Attack;
    public AudioClip Father_Damaged;
    public AudioClip Father_Death;


    public void PlaySFXFather(AudioClip clip)
    {
        m_SFXSourceFather.PlayOneShot(clip);
    }
}
