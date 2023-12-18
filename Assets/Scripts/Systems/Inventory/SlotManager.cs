using Inventory.UI;
using System;
using System.Collections.Generic;
using UnityEngine;


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
                if (inventoryItems[i].item != null && inventoryItems[i].item.ItemImage == item.ItemImage)
                {
                    return i;
                }
            }
            return -1;
        }
        public void ChangeAmount(int slotIndex, int amount)
        {
            InventoryItem copy = inventoryItems[slotIndex];
            copy.quantity += amount;
            inventoryItems[slotIndex] = copy;
            UIInventory.Instance.UpdateData(slotIndex, inventoryItems[slotIndex].item.ItemImage, inventoryItems[slotIndex].quantity);
        }
        public int GetFirstEmptySlotIndex()
        {
            for (int i = 0; i < inventoryItems.Count; i++)
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
                PlayerUISlotsManager.Instance.TryAutoEquipWeapon(((Weapon)item).CanUse, (Weapon)item);
            }
            else if (item.index == 1|| item.index == 2)
            {
                PlayerUISlotsManager.Instance.AutoEquipUtility(item);
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
            for(int i =0;i<inventoryItems.Count;i++)
            {
                if (inventoryItems[i].item?.index == index)
                {
                    inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity - 1);
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
            if (newQuantity <= 0)
            {
                UIInventory.Instance.RemoveItem(this.item);
            }
            return new InventoryItem()
            {
                item = newQuantity != 0?this.item:null,
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