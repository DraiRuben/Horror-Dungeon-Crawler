using Inventory.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Inventory.Model
{
    [CreateAssetMenu(menuName = "UIItems")]
    public class SlotManager : ScriptableObject
    {
        public List<InventoryItem> inventoryItems;

        private InventoryItem[] inventoryItemsInstance;
        [field: SerializeField] public int Size { get; private set; } = 10;

        public void Initialize()
        {
            inventoryItemsInstance = new InventoryItem[inventoryItems.Count];
        }
        public int GetDuplicateSlotIndex(Item item)
        {
            for (int i = 0; i < inventoryItemsInstance.Length; i++)
            {
                if (inventoryItemsInstance[i].item != null && inventoryItemsInstance[i].item.ItemImage == item.ItemImage)
                {
                    return i;
                }
            }
            return -1;
        }
        public void ChangeAmount(int slotIndex, int amount)
        {
            InventoryItem copy = inventoryItemsInstance[slotIndex];
            copy.quantity += amount;
            inventoryItemsInstance[slotIndex] = copy;
            UIInventory.Instance.UpdateData(slotIndex, inventoryItemsInstance[slotIndex].item.ItemImage, inventoryItemsInstance[slotIndex].quantity);
        }
        public int GetFirstEmptySlotIndex()
        {
            for (int i = 0; i < inventoryItemsInstance.Length; i++)
            {
                if (inventoryItemsInstance[i].isEmpty)
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
                inventoryItemsInstance[slotIndex] = new InventoryItem
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
            return inventoryItemsInstance[itemIndex];
        }

        public void RemoveItem(Item item)
        {
            for (int i = 0; i < inventoryItemsInstance.Length; i++)
            {
                if (inventoryItemsInstance[i].item == item)
                {
                    inventoryItemsInstance[i] = InventoryItem.GetEmptyItem();
                    break;
                }
            }
        }

        public bool UseItemByIndex(int index)
        {
            for(int i =0;i<inventoryItemsInstance.Length;i++)
            {
                if (inventoryItemsInstance[i].item?.index == index)
                {
                    inventoryItemsInstance[i] = inventoryItemsInstance[i].ChangeQuantity(inventoryItemsInstance[i].quantity - 1);
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