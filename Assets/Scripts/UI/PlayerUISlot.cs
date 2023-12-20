using Inventory.Model;
using System;
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
