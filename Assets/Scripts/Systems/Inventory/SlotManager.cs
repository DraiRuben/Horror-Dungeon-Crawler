using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.Remoting.Messaging;
using Inventory.UI;
using Unity.VisualScripting;


namespace Inventory.Model
{
    [CreateAssetMenu(menuName = "UIItems")]
    public class SlotManager : ScriptableObject
    {
        [SerializeField] private List<InventoryItem> inventoryItems;
        [field: SerializeField] public int Size { get; private set; } = 10;

        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }
        public int GetDuplicateSlotIndex(Item item)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].item !=null && inventoryItems[i].item.ItemImage == item.ItemImage)
                {
                    return i;
                }
            }
            return -1;
        }
        public void ChangeAmount(int slotIndex, int amount)
        {
            var copy = inventoryItems[slotIndex];
            copy.quantity += amount;
            inventoryItems[slotIndex] = copy;
            UIInventory.Instance.UpdateData(slotIndex, inventoryItems[slotIndex].item.ItemImage, inventoryItems[slotIndex].quantity);
        }
        public int GetFirstEmptySlotIndex()
        {
            for(int i  = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].isEmpty)
                {
                    return i;
                }
            }
            return -1;
        }
        public void AddItem(Item item, int quantity)
        {
            int slotIndex = GetFirstEmptySlotIndex();
            if (slotIndex != -1)
            {
                inventoryItems[slotIndex] = new InventoryItem
                {
                    item = item,
                    quantity = quantity
                };
            }
            if (item.GetType() == typeof(Weapon) && ((Weapon)item).AutoEquip)
            {
                PlayerUISlotsManager.Instance.TryAutoEquip(((Weapon)item).CanUse, (Weapon)item);
            }
        }

        public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        public void RemoveItem(Item item)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].item == item)
                {
                    inventoryItems[i] = InventoryItem.GetEmptyItem();
                    break;
                }
            }
        }

        public bool UseItemByIndex(int index)
        {
            foreach (InventoryItem inventoryItem in inventoryItems)
            {
                if (inventoryItem.item?.index == index)
                {
                    inventoryItem.ChangeQuantity(inventoryItem.quantity - 1);
                    return true;
                }
            }
            return false;
        }

    }

    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public Item item;

        public bool isEmpty => item == null;

        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem()
            {
                item = this.item,
                quantity = newQuantity,
            };
        }

        public static InventoryItem GetEmptyItem()
        => new InventoryItem
        {
            item = null,
            quantity = 0,
        };
    }

}