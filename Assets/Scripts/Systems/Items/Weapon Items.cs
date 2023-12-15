using Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class Weapon : Item
    {

        public Character CanUse;
        public bool AutoEquip;

        public bool ShootsProjectile;
        public float ProjectileSpeed;

        public int Range;
        public int Damage;
        public float ReloadTime;

        [NonSerialized] public float previousTimeUsed;
        public override bool Use()
        {
            if(Time.time-previousTimeUsed > ReloadTime)
            {
                var Player = PlayerMovement.Instance;
                var AttackDir = MapGrid.Instance.GetRelativeDir(MapGrid.AllowedMovesMask.Top, Player.transform.rotation.eulerAngles.y);
                if (ShootsProjectile)
                {
                    AttackSystem.Instance.RangedAttack(Player.GridPos, Player.CurrentFloor, AttackDir, Damage, Range, ProjectileSpeed);
                }
                else
                {
                    AttackSystem.Instance.CQCAttack(Player.GridPos, Player.CurrentFloor, AttackDir, Damage);
                }
                previousTimeUsed = Time.time;
                return true;
            }
            return false;
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