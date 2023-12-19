using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : EntityStats
{
    private int m_currentStress;
    [SerializeField] private int m_maxStress = 100;

    [SerializeField] private Image HealthBarPlayer;

    void Start()
    {
        CurrentHealth = m_maxHealth;
        m_currentStress = m_maxStress;
        HealthBarPlayer.fillAmount = 1;
        OnHealthChanged.AddListener(PlayerUpdateFill);
    }
    private void OnEnable()
    {
        var connected = UIPlayerFormation.Instance.Previewers.First(x => x.m_linkedCharacter == this);
        connected.m_image.color = Color.white;
        connected.m_linkedElement.m_image.color = Color.white;
    }
    /*public void TakeStress(int stress)
    {
        m_curStress -= stress;
        //PlayerStressUI.Instance.PlayerUpdateStress((float)m_curStress / m_maxStress);

    }*/

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
    public void PlayerUpdateFill()
    {
        HealthBarPlayer.fillAmount = (float)CurrentHealth / m_maxHealth;
    }
}
