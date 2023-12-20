using Inventory.Model;
using Sirenix.OdinInspector;
using System;
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
        bool itemNotInInventory = Inventory.Inventory.Instance.inventoryData.GetDuplicateSlotIndex(_toEquip) == -1;

        if (_toEquip.index == 1)
        {
            if (!itemNotInInventory)
            {
                medKitSlot.CurrentItem = _toEquip;
                medKitSlot.SlotImage.sprite = _toEquip.ItemImage;
                medKitSlot.SlotImage.color = Color.white;
                medKitSlot.gameObject.SetActive(true);
            }
            else 
            {
                medKitSlot.CurrentItem = null;
                medKitSlot.SlotImage.sprite = null;
                medKitSlot.SlotImage.color = Color.clear;
                medKitSlot.gameObject.SetActive(false);
            }
        }
        else if (_toEquip.index == 2)
        {
            if (!itemNotInInventory)
            {
                pillsSlot.CurrentItem = _toEquip;
                pillsSlot.SlotImage.sprite = _toEquip.ItemImage;
                pillsSlot.SlotImage.color = Color.white;
                pillsSlot.gameObject.SetActive(true);
            }
            else
            {
                pillsSlot.CurrentItem = null;
                pillsSlot.SlotImage.sprite = null;
                pillsSlot.SlotImage.color = Color.clear;
                pillsSlot.gameObject.SetActive(false);
            }
        }
    }

    public void Equip(Weapon.Character _character, Weapon _toEquip)
    {
        _toEquip.previousTimeUsed = Time.time;
        WeaponSlots[_character].CurrentItem = _toEquip;
        WeaponSlots[_character].SlotImage.sprite = _toEquip.ItemImage;
    }
}
