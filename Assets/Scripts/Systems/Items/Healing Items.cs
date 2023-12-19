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

    public void Heal()
    {
        stats.CurrentHealth += 50;
    }

    public void Pills()
    {
        stats.CurrentStress -= 50;
    }
}
