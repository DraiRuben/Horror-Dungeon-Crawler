using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityStats : MonoBehaviour
{
    [SerializeField] private int m_maxHealth = 100;
    private int m_curHealth;
    [SerializeField] private bool m_isBoss;


    void Start()
    {
        m_curHealth = m_maxHealth;
    }

    public void TakeDamage(int damage)
    {
        m_curHealth -= damage;
        BossHealthUI.Instance.BossUpdateFill( (float)m_curHealth / m_maxHealth);
       
    }
}