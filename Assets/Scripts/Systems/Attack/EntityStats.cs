using System;
using UnityEngine;
using UnityEngine.Events;

public class EntityStats : MonoBehaviour
{
    [SerializeField] protected int m_maxHealth = 100;
    public int Dexterity;
    public int Strength;
    protected int m_health;
    public int CurrentHealth { get { return m_health; } set { m_health = value; OnHealthChanged.Invoke(); } }

    [SerializeField] protected bool m_isBoss;

    [NonSerialized] public UnityEvent OnHealthChanged = new();

    void Start()
    {
        CurrentHealth = m_maxHealth;
    }

    public void TakeDamage(int damage)
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