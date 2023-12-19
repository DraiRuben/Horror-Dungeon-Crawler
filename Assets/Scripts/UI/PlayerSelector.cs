using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerSelector : Selectable
{
    public static PlayerStats CurrentlySelected;

    [SerializeField] private PlayerStats toSelect;
    private void Start()
    {
        
    }
    public override void OnDeselect(BaseEventData eventData)
    {
        
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (CurrentlySelected == toSelect)
        {
            CurrentlySelected = null;
            base.OnDeselect(eventData);
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            CurrentlySelected = toSelect;
        }
    }
}
