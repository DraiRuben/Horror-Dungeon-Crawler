using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioFather : MonoBehaviour
{
    private AudioManagerFather m_audioManager;
    private float minInterval = 0.0f;
    private float maxInterval = 3.0f;
    private float offset = 3.0f;
    private Boss1AI bossAI;
    private void Awake()
    {
        m_audioManager = transform.GetChild(0).GetComponent<AudioManagerFather>();
    }

    private void Start()
    {
        Invoke("FatherIdle", 2.0f);
    }
    
    public void FatherDamagedSound()
    {
        m_audioManager.PlaySFXFather(m_audioManager.Father_Damaged);
    }

    public void FatherDeathSound()
    {
        m_audioManager.PlaySFXFather(m_audioManager.Father_Death);
    }

    public void FatherIdle()
    {
        float randomInterval = Random.Range(minInterval, maxInterval) + offset;
        m_audioManager.PlaySFXFather(m_audioManager.Father_Phase1_Idle);
        Invoke("FatherIdle", randomInterval);
    }
}
