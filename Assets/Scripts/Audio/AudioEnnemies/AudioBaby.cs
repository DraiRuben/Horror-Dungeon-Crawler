using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioBaby : MonoBehaviour
{
    private AudioManagerBaby m_audioManager;
    private float minInterval = 0.0f;
    private float maxInterval = 3.0f;
    private float offset = 3.0f;
    private Boss1AI bossAI;
    private void Awake()
    {
        m_audioManager = transform.GetChild(0).GetComponent<AudioManagerBaby>();
    }

    private void Start()
    {
        Invoke("BabyIdle", 2.0f);
    }
    
    public void BabyDamagedSound()
    {
        m_audioManager.PlaySFXBaby(m_audioManager.Baby_Damaged);
    }

    public void BabyDeathSound()
    {
        m_audioManager.PlaySFXBaby(m_audioManager.Baby_Death);
    }

    public void BabyIdle()
    {
        float randomInterval = Random.Range(minInterval, maxInterval) + offset;
        m_audioManager.PlaySFXBaby(m_audioManager.Baby_Phase1_Idle);
        Invoke("BabyIdle", randomInterval);
    }
}
