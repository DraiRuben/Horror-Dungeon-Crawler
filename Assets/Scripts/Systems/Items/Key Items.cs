using Inventory.UI;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class Key : Item
    {
        public override bool Use()
        {
            Quantity--; 

            if (Quantity <= 0)
            {
                Inventory.Instance.inventoryData.RemoveItem(this);
                UIInventory.Instance.RemoveItem(this);
            }
            m_audioManager.PlaySFX(UseSFX);
            return true;
        }
    }
}