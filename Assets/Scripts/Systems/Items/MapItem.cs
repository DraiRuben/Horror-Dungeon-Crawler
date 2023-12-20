using Inventory.UI;
using UnityEngine;

namespace Inventory.Model
{
    public class MapItem : MonoBehaviour
    {
        public Item itemSO;
        private int itemQuantity = 1;
        private AudioManager m_audioManager;

        private void Awake()
        {
            m_audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        }
        public void PickUpItemOnMap()
        {
            if (itemSO.IsStackable)
            {
                int duplicateSlotIndex = Inventory.Instance.inventoryData.GetDuplicateSlotIndex(itemSO);
                if (duplicateSlotIndex >= 0)
                {
                    Inventory.Instance.inventoryData.ChangeAmount(duplicateSlotIndex, 1);
                    Destroy(gameObject);
                    return;
                }
            }

            int newSlotIndex = Inventory.Instance.inventoryData.GetFirstEmptySlotIndex();
            if (newSlotIndex >= 0)
            {
                UIInventory.Instance.UpdateData(newSlotIndex, itemSO.ItemImage, itemQuantity);
                Inventory.Instance.inventoryData.AddItem(itemSO, 1);
                Destroy(gameObject);
                return;
            }
            m_audioManager.PlaySFX(m_audioManager.Pickup_Items);
        }

        private void OnMouseDown()
        {
            PickUpItemOnMap();
        }
    }
}