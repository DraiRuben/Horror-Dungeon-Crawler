using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EntityStats : MonoBehaviour
{
    [SerializeField] protected int m_maxHealth = 100;
    protected int m_currentHealth{  get { return m_currentHealth; } set {  m_currentHealth = value; OnHealthChanged.Invoke(); } }

    [SerializeField] protected bool m_isBoss;

    public UnityEvent OnHealthChanged = new();

    void Start()
    {
        m_currentHealth = m_maxHealth;
    }

    public void TakeDamage(int damage)
    {
        m_currentHealth -= damage;
        if (m_isBoss)
        {
            BossHealthUI.Instance.BossUpdateFill((float)m_currentHealth / m_maxHealth);
        }
    }
}