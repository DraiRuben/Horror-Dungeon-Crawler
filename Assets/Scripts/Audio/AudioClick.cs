using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClick : MonoBehaviour
{
    private AudioManager m_audioManager;

    private void Awake()
    {
        m_audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void ClickSFX()
    {
        m_audioManager.PlaySFX(m_audioManager.Click_Button);
    }
}
