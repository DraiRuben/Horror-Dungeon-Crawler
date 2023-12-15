using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUISlot : MonoBehaviour
{
    [NonSerialized] public Image SlotImage;
    public Item CurrentItem;
    private void Awake()
    {
        SlotImage = GetComponent<Image>();
    }
    public void Use()
    {
        CurrentItem?.Use();
    }
}
