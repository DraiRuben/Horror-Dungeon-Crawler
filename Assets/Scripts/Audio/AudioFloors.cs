using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFloors : SerializedMonoBehaviour
{

    public Dictionary<int, AudioClip> AudioFloor;

    private void LevelsAmbientMusic(int Floor)
    {
        AudioManager.Instance.PlayMusic(AudioFloor[Floor]);
    }
}

