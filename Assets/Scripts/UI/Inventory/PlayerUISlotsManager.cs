using Inventory.Model;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            WeaponSlots[_character].CurrentItem = _toEquip;
            WeaponSlots[_character].SlotImage.sprite = _toEquip.ItemImage;
        }
    }
    public void Equip(Weapon.Character _character, Weapon _toEquip)
    {
        WeaponSlots[_character].CurrentItem = _toEquip;
        WeaponSlots[_character].SlotImage.sprite = _toEquip.ItemImage;
    }
    public class PlayerUISlot
    {
        public Image SlotImage;
        public Item CurrentItem;
    }
}
