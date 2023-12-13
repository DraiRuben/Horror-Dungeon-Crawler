using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    public class Key : Item
    {
        [SerializeField] public int usesLeft;

        public void UseKey()
        {
            usesLeft--;
            if (usesLeft <= 0)
            {
                //SlotManager.RemoveItem(Item item);
            }
        }
    }
}