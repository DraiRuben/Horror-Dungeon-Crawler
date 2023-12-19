using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItems : MonoBehaviour
{
    public bool isMedKit;
    public bool isPills;
    private PlayerStats stats;

    private void Start()
    {
        stats = GetComponent<PlayerStats>();
    }

    private void Heal()
    {
        if (isMedKit)
        {
            stats.CurrentHealth += 50;
        }
        else if (isPills)
        {
            stats.CurrentStress -= 50;
        }
    }
}
