using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("------------AudioSource------------")]

    [SerializeField] AudioSource m_musicSource;
    [SerializeField] AudioSource m_SFXSource;

    [Header("------------AudioClip------------")]

    public AudioClip Footstep_Concrete;
    public AudioClip Footstep_Wood;
    public AudioClip Click_Button;

    [Header("Items")]

    public AudioClip Pickup_Items;
    public AudioClip Door;
    public AudioClip Prison_Door;
    public AudioClip Corpses_Burning;

    [Header("Mobs")]

    public AudioClip Dog_Idle;
    public AudioClip Dog_Attack;
    public AudioClip Dog_Damaged;
    public AudioClip Dog_Death;

    [Header("Boss")]

    public AudioClip Father_Phase1_Idle;
    public AudioClip Father_Phase2_Idle;
    public AudioClip Father_Attack;
    public AudioClip Father_Damaged;
    public AudioClip Father_Death;

    [Header("------------Music------------")]

    public AudioClip Main_Menu;
    public AudioClip Victory_Menu;
    public AudioClip Defeat_Menu;
    public AudioClip Basement;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void PlaySFX(AudioClip clip)
    {
        m_SFXSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip music)
    {
        m_musicSource.PlayOneShot(music);
        m_musicSource.loop = true;
    }

    internal void PlayMusic(Dictionary<int, AudioClip> audioFloor)
    {
        throw new NotImplementedException();
    }
}
