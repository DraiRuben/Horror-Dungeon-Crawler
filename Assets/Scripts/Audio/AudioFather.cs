using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioFather : MonoBehaviour
{
    private float minInterval = 0.0f;
    private float maxInterval = 3.0f;
    private float offset = 3.0f;
    private Boss1AI bossAI;

    private void Start()
    {
        Invoke(nameof(FatherIdle), 2.0f);
    }
    
    public void FatherDamagedSound()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.Father_Damaged);
    }

    public void FatherDeathSound()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.Father_Death);
    }

    public void FatherIdle()
    {
        float randomInterval = Random.Range(minInterval, maxInterval) + offset;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.Father_Phase1_Idle);
        Invoke(nameof(FatherIdle), randomInterval);
    }
}
