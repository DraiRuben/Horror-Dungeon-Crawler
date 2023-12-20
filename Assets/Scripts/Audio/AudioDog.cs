using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDog : MonoBehaviour
{

    private float minInterval = 0.0f;
    private float maxInterval = 3.0f;
    private float offset = 3.0f;
    private void Start()
    {
        Invoke(nameof(DogIdle), 2.0f);
    }

    public void DogDamagedSound()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.Dog_Damaged);
    }

    public void DogDeathSound()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.Dog_Death);
    }

    public void DogIdle()
    {
        float randomInterval = Random.Range(minInterval, maxInterval) + offset;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.Dog_Idle);
        Invoke("DogIdle", randomInterval);
    }
}
