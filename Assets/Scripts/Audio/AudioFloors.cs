using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFloors : SerializedMonoBehaviour
{
    private AudioManager m_audioManager;

    public Dictionary<int, AudioClip> AudioFloor;
    private void Awake()
    {
        m_audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void LevelsAmbientMusic(int Floor)
    {
        m_audioManager.PlayMusic(AudioFloor[Floor]);
    }
}

