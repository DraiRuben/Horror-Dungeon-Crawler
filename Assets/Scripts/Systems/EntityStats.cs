using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityStats : MonoBehaviour
{
    [SerializeField] private int m_maxHealth = 100;
    private int m_currentHealth;
    [SerializeField] private bool m_isBoss;


    void Start()
    {
        m_currentHealth = m_maxHealth;
    }

    public void TakeDamage(int damage)
    {
        m_currentHealth -= damage;

        if(m_isBoss)
            BossHealthUI.Instance.BossUpdateFill( (float)m_currentHealth / m_maxHealth);
    }
}