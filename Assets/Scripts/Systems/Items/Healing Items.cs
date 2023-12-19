using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItems : MonoBehaviour
{
    public void Heal()
    {
        if(PlayerSelector.CurrentlySelected != null && PlayerSelector.CurrentlySelected.CurrentHealth > 0)
        {
            PlayerSelector.CurrentlySelected.CurrentHealth += 50;
            Inventory.Inventory.Instance.inventoryData.UseItemByIndex(1);
        }
    }

    public void Pills()
    {
        if (PlayerSelector.CurrentlySelected != null && PlayerSelector.CurrentlySelected.CurrentHealth > 0)
        {
            PlayerSelector.CurrentlySelected.CurrentStress -= 50;
            Inventory.Inventory.Instance.inventoryData.UseItemByIndex(2);
        }
    }
}
