using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    public class MapItem : MonoBehaviour
    {
        public Item itemSO;
        private int itemQuantity = 1;

        public void PickUpItemOnMap()
        {
            if (itemSO.IsStackable)
            {
                int duplicateSlotIndex = InventoryManager.Instance.inventoryData.GetDuplicateSlotIndex(itemSO);
                if (duplicateSlotIndex >= 0)
                {
                    InventoryManager.Instance.inventoryData.ChangeAmount(duplicateSlotIndex, 1);
                    Destroy(gameObject);
                    return;
                }
            }

            int newSlotIndex = InventoryManager.Instance.inventoryData.GetFirstEmptySlotIndex();
            if (newSlotIndex >= 0)
            {
                UIInventoryScript.Instance.UpdateData(newSlotIndex, itemSO.ItemImage, itemQuantity);
                InventoryManager.Instance.inventoryData.AddItem(itemSO, 1);
                Destroy(gameObject);
                return;
            }
        }

        private void OnMouseDown()
        {
            PickUpItemOnMap();
        }
    }
}