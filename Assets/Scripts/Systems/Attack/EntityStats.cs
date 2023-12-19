using System;
using UnityEngine;
using UnityEngine.Events;

public class EntityStats : MonoBehaviour
{
    [SerializeField] protected int m_maxHealth = 100;
    public int Dexterity;
    public int Strength;
    protected int m_health;

    [SerializeField] protected int m_maxStress = 100;
    protected int m_stress;
    public int CurrentHealth { get { return m_health; } set { m_health = value; if (value > 0) OnHealthChanged.Invoke(); else OnDeath.Invoke(); } }
    public int CurrentStress { get { return m_stress; } set { m_stress = value; if (value > 0) OnMaxStressReached.Invoke(); } }

    [SerializeField] protected bool m_isBoss;

     public UnityEvent OnHealthChanged = new();
     public UnityEvent OnMaxStressReached = new();
     public UnityEvent OnDeath = new();

    void Start()
    {
        CurrentHealth = m_maxHealth;
        CurrentStress = 0;
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

    public virtual void TakeStress(int stressD)
    {
        CurrentStress -= stressD;
        if (CurrentStress <= 0)
        {
            TakeDamage(50);
        }
    }
}