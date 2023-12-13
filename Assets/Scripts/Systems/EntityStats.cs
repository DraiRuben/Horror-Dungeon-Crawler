using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EntityStats : MonoBehaviour
{
    [SerializeField] protected int m_maxHealth = 100;
    public int Dexterity;
    public int Strength;
    protected int m_health;
    protected int m_currentHealth{  get { return m_health; } set {  m_health = value; OnHealthChanged.Invoke(); } }

    [SerializeField] protected bool m_isBoss;

    [NonSerialized]public UnityEvent OnHealthChanged = new();

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