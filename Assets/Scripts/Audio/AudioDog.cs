using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDog : MonoBehaviour
{

    private AudioManagerEnnemies m_audioManager;
    private float minInterval = 0.0f;
    private float maxInterval = 3.0f;
    private float offset = 3.0f;
    private void Awake()
    {
        m_audioManager = transform.GetChild(0).GetComponent<AudioManagerEnnemies>();  
    }

    private void Start()
    {
        Invoke("DogIdle", 2.0f);
    }

    public void DogDamagedSound()
    {
        m_audioManager.PlaySFXMob(m_audioManager.Dog_Damaged);
    }

    public void DogDeathSound()
    {
        m_audioManager.PlaySFXMob(m_audioManager.Dog_Death);
    }

    public void DogIdle()
    {
        float randomInterval = Random.Range(minInterval, maxInterval) + offset;
        m_audioManager.PlaySFXMob(m_audioManager.Dog_Idle);
        Invoke("DogIdle", randomInterval);
    }
}
