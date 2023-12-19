using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStats : EntityStats
{
    [SerializeField] private Image HealthBarPlayer;
    [SerializeField] private Image StressBarPlayer;

    private const int StressDamage = 50;
    private const float StressIncreaseRate = 0.1f;
    private WaitForSeconds stressIncreaseDelay = new WaitForSeconds(1.0f);

    private Coroutine stressCoroutine;

    void Start()
    {
        CurrentHealth = m_maxHealth;
        CurrentStress = 0;
        HealthBarPlayer.fillAmount = 1;
        if (StressBarPlayer != null )
        {
            StressBarPlayer.fillAmount = 0;
        }
        OnHealthChanged.AddListener(PlayerUpdateFill);
        stressCoroutine = StartCoroutine(IncreaseStress());
    }


    public override void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (m_isBoss)
        {
            BossHealthUI.Instance.BossUpdateFill((float)CurrentHealth / m_maxHealth);
        }
        if (CurrentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator IncreaseStress()
    {
        while (true)
        {
            yield return stressIncreaseDelay;

            CurrentStress++;
            if (StressBarPlayer != null)
            {
                StressBarPlayer.fillAmount = (float)CurrentStress / m_maxStress;
                if (CurrentStress >= m_maxStress)
                {
                    TakeDamage(StressDamage);
                    CurrentStress = 0;
                    StressBarPlayer.fillAmount = 0;
                }
            }
        }
    }

    public void PlayerUpdateFill()
    {
        HealthBarPlayer.fillAmount = (float)CurrentHealth / m_maxHealth;
        StressBarPlayer.fillAmount = (float)CurrentStress / m_maxStress;
    }

    public void SelectedCharacter()
    {

    }
}
