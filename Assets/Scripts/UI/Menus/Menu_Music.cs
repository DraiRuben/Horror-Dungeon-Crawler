using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Music : MonoBehaviour
{

    public void Start()
    {
        AudioManager.Instance.PlayMusic(AudioManager.Instance.Main_Menu);
    }
}
