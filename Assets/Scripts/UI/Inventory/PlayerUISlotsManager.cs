using Inventory.Model;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUISlotsManager : SerializedMonoBehaviour
{
    public Dictionary<Weapon.Character, PlayerUISlot> WeaponSlots;
    public Dictionary<Weapon.Character, PlayerUISlot> AbilitySlots;
    public Dictionary<Weapon.Character, PlayerUISlot> UtilitySlots;

    public static PlayerUISlotsManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void TryAutoEquip(Weapon.Character _character, Weapon _toEquip)
    {
        if (WeaponSlots[_character].CurrentItem == null)
        {
            _toEquip.previousTimeUsed = Time.time;
            WeaponSlots[_character].CurrentItem = _toEquip;
            WeaponSlots[_character].SlotImage.sprite = _toEquip.ItemImage;
        }
    }
    public void Equip(Weapon.Character _character, Weapon _toEquip)
    {
        _toEquip.previousTimeUsed = Time.time;
        WeaponSlots[_character].CurrentItem = _toEquip;
        WeaponSlots[_character].SlotImage.sprite = _toEquip.ItemImage;
    }
}
