using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics.Eventing.Reader;


namespace Inventory.Model
{
    [CreateAssetMenu(menuName = "UIItems")]
    public class SlotManager : ScriptableObject
    {
        [SerializeField] private List<InventoryItem> inventoryItems;
        [field: SerializeField] public int Size { get; private set; } = 10;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;
        public event Action OnInventoryChanged;

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

                InformAboutChange();
            }
        }

        public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (!inventoryItems[i].isEmpty)
                {
                    returnValue[i] = inventoryItems[i];
                }
            }
            return returnValue;
        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            (inventoryItems[itemIndex_2], inventoryItems[itemIndex_1]) = (inventoryItems[itemIndex_1], inventoryItems[itemIndex_2]);
            InformAboutChange();
        }

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
            OnInventoryChanged?.Invoke();
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