using UnityEngine;

public class AudioBaby : MonoBehaviour
{
    private AudioManagerBaby m_audioManager;
    private readonly float minInterval = 0.0f;
    private readonly float maxInterval = 3.0f;
    private readonly float offset = 3.0f;
    public Boss1AI boss1AI;

    private void Awake()
    {
        m_audioManager = transform.GetChild(0).GetComponent<AudioManagerBaby>();
    }
    private void Start()
    {
        Invoke(nameof(BabyIdle), 2.0f);
    }

    public void BabyDamagedSound()
    {
        if (!boss1AI.father_Phase2)
        {
            m_audioManager.PlaySFXBaby(m_audioManager.Baby_Phase1_Damaged);
        }
        else
        {
            m_audioManager.PlaySFXBaby(m_audioManager.Baby_Phase2_Damaged);
        }
    }

    public void BabyDeathSound()
    {
        m_audioManager.PlaySFXBaby(m_audioManager.Baby_Death);
    }

    public void BabyIdle()
    {
        if (!boss1AI.father_Phase2)
        {
            float randomInterval = Random.Range(minInterval, maxInterval) + offset;
            m_audioManager.PlaySFXBaby(m_audioManager.Baby_Phase1_Idle);
            Invoke(nameof(BabyIdle), randomInterval);
        }
        else
        {
            float randomInterval = Random.Range(minInterval, maxInterval) + offset;
            m_audioManager.PlaySFXBaby(m_audioManager.Baby_Phase2_Idle);
            Invoke(nameof(BabyIdle), randomInterval);
        }
    }
}
