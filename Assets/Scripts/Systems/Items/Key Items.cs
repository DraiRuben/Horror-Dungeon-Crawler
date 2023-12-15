using Inventory;
using System.Collections;
using System.Collections.Generic;
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
            return true;
        }
    }
}