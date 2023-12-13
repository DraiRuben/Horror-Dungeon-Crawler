using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : EntityStats
{
    private int m_currentStress;
    [SerializeField] private int m_maxStress= 100;

    [SerializeField] private Image HealthBarPlayer;
   
    void Start()
    {
        m_currentHealth = m_maxHealth;
        m_currentStress = m_maxStress;
        HealthBarPlayer.fillAmount = 1;
        OnHealthChanged.AddListener(PlayerUpdateFill);
    }

    /*public void TakeStress(int stress)
    {
        m_curStress -= stress;
        //PlayerStressUI.Instance.PlayerUpdateStress((float)m_curStress / m_maxStress);

    }*/

    
    public void PlayerUpdateFill()
    {
        HealthBarPlayer.fillAmount = (float) m_currentHealth / m_maxHealth;
    }
}
