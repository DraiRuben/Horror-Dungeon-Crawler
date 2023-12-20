using UnityEngine;

public class HealingItems : MonoBehaviour
{
    [SerializeField] private PlayerUISlot pillsSlot, medKitSlot;


    public void Heal()
    {
        if (PlayerSelector.CurrentlySelected != null && PlayerSelector.CurrentlySelected.CurrentHealth > 0)
        {
            PlayerSelector.CurrentlySelected.CurrentHealth += 50;
            Inventory.Inventory.Instance.inventoryData.UseItemByIndex(1);
            if (Inventory.Inventory.Instance.inventoryData.GetItemInInventoryByIndex(1).quantity <= 0)
            {
                medKitSlot.gameObject.SetActive(false);
                medKitSlot.CurrentItem = null;
            }
        }
    }

    public void Pills()
    {
        if (PlayerSelector.CurrentlySelected != null && PlayerSelector.CurrentlySelected.CurrentHealth > 0)
        {
            PlayerSelector.CurrentlySelected.CurrentStress -= 50;
            Inventory.Inventory.Instance.inventoryData.UseItemByIndex(2);
            if (Inventory.Inventory.Instance.inventoryData.GetItemInInventoryByIndex(2).quantity <= 0)
            {
                pillsSlot.gameObject.SetActive(false);
                pillsSlot.CurrentItem = null;
            }
        }
    }
}
