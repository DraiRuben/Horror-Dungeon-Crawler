using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class AudioFloors : SerializedMonoBehaviour
{

    public Dictionary<int, AudioClip> AudioFloor;
    private void Start()
    {
        PlayerMovement.Instance.OnFloorChanged += LevelsAmbientMusic;
        LevelsAmbientMusic(0);
    }
    private void LevelsAmbientMusic(int Floor)
    {
        AudioManager.Instance.PlayMusic(AudioFloor[Floor]);
    }
}

