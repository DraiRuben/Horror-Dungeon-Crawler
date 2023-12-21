using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSister : MonoBehaviour
{
    private AudioManagerSister m_audioManager;
    private float minInterval = 0.0f;
    private float maxInterval = 3.0f;
    private float offset = 3.0f;
    private void Awake()
    {
        m_audioManager = transform.GetChild(0).GetComponent<AudioManagerSister>();
    }

    private void Start()
    {
        Invoke("SisterIdle", 2.0f);
    }

    public void SisterDamagedSound()
    {
        m_audioManager.PlaySFXSister(m_audioManager.Sister_Damaged);
    }

    public void SisterDeathSound()
    {
        m_audioManager.PlaySFXSister(m_audioManager.Sister_Death);
    }

    public void SisterIdle()
    {
        float randomInterval = Random.Range(minInterval, maxInterval) + offset;
        m_audioManager.PlaySFXSister(m_audioManager.Sister_Idle);
        Invoke("DogIdle", randomInterval);
    }
}
