using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LinkedSlot : MonoBehaviour
{
    public Image SlotImage;
    public TMP_Text QuantityText;

    public Item LinkedItem;

    public static List<LinkedSlot> LinkedSlots;

    private void Awake()
    {
        if(LinkedSlots == null) LinkedSlots = new List<LinkedSlot>();

        LinkedSlots.Add(this);
    }


}
