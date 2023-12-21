using UnityEngine;

public class AudioDog : MonoBehaviour
{

    private AudioManagerEnnemies m_audioManager;
    private readonly float minInterval = 0.0f;
    private readonly float maxInterval = 3.0f;
    private readonly float offset = 3.0f;
    private void Awake()
    {
        m_audioManager = transform.GetChild(0).GetComponent<AudioManagerEnnemies>();
    }
    private void Start()
    {

        Invoke(nameof(DogIdle), 2.0f);
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
        Invoke(nameof(DogIdle), randomInterval);
    }
}
