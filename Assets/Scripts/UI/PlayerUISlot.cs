using Inventory.Model;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUISlot : MonoBehaviour
{
    public Image SlotImage;
    public Item CurrentItem;
    public void Use()
    {
        CurrentItem?.Use();
    }
}
