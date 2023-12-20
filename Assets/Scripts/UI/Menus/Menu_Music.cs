using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Music : MonoBehaviour
{
    private AudioManager audioManager;
    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void Start()
    {
        audioManager.PlayMusic(audioManager.main_Menu);
    }
}
