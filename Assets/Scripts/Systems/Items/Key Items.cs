using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class Key : Item
    {
        public int usesLeft;

        public override bool Use()
        {
            usesLeft--;
            if (usesLeft <= 0)
            {
                //SlotManager.RemoveItem(Item item);
            }
            m_audioManager.PlaySFX(UseSFX);
            return true;
        }
    }
}