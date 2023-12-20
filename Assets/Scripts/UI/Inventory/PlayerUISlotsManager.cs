using Inventory.Model;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUISlotsManager : SerializedMonoBehaviour
{
    public Dictionary<Weapon.Character, PlayerUISlot> WeaponSlots;

    public PlayerUISlot medKitSlot;
    public PlayerUISlot pillsSlot;

    public static PlayerUISlotsManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void TryAutoEquipWeapon(Weapon.Character _character, Weapon _toEquip)
    {
        if (WeaponSlots[_character].CurrentItem == null)
        {
            _toEquip.previousTimeUsed = Time.time;
            WeaponSlots[_character].CurrentItem = _toEquip;
            WeaponSlots[_character].SlotImage.sprite = _toEquip.ItemImage;
        }
    }

    public void AutoEquipUtility(Item _toEquip)
    {
        if (_toEquip.index == 1)
        {
            medKitSlot.CurrentItem = _toEquip;
            medKitSlot.SlotImage.sprite = _toEquip.ItemImage;
            medKitSlot.SlotImage.color = Color.white;
        }
        else if (_toEquip.index == 2)
        {
           pillsSlot.CurrentItem = _toEquip;
           pillsSlot.SlotImage.sprite = _toEquip.ItemImage;
           pillsSlot.SlotImage.color = Color.white;
        }
    }

    public void Equip(Weapon.Character _character, Weapon _toEquip)
    {
        _toEquip.previousTimeUsed = Time.time;
        WeaponSlots[_character].CurrentItem = _toEquip;
        WeaponSlots[_character].SlotImage.sprite = _toEquip.ItemImage;
    }
}
