using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : EntityStats
{
    private int m_currentStress;
    [SerializeField] private int m_maxStress= 100;
    
    void Start()
    {
        m_currentStress = m_maxStress;
    }

    /*public void TakeStress(int stress)
    {
        m_curStress -= stress;
        //PlayerStressUI.Instance.PlayerUpdateStress((float)m_curStress / m_maxStress);

    }*/
}
