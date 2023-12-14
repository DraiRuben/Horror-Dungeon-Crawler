using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class Weapon : Item
    {
        public int Damage;
        public float ReloadTime;
        public Character CanUse;
        public bool AutoEquip;
        public void UseWeapon()
        {
            
        }

        public enum Character
        {
            Olga,
            Morgane,
            Larry,
            Brother
        }
    }
}