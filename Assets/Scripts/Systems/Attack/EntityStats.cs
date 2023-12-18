using System;
using UnityEngine;
using UnityEngine.Events;

public class EntityStats : MonoBehaviour
{
    [SerializeField] protected int m_maxHealth = 100;
    public int Dexterity;
    public int Strength;
    protected int m_health;
    public int CurrentHealth { get { return m_health; } set { m_health = value; if (value > 0) OnHealthChanged.Invoke(); else OnDeath.Invoke(); } }

    [SerializeField] protected bool m_isBoss;

     public UnityEvent OnHealthChanged = new();
     public UnityEvent OnDeath = new();

    void Start()
    {
        CurrentHealth = m_maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (m_isBoss)
        {
            BossHealthUI.Instance.BossUpdateFill((float)CurrentHealth / m_maxHealth);
        }
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}