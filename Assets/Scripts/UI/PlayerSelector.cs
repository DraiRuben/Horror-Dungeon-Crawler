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
    public override void OnDeselect(BaseEventData eventData)
    {
        var castedData = (PointerEventData)eventData;
        if (castedData.pointerEnter == null || !castedData.pointerEnter.CompareTag("HealingItems"))
        {
            base.OnDeselect(eventData);
            if (CurrentlySelected == toSelect)
            {
                CurrentlySelected = null;
            }
            Debug.Log("Deselected");
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (CurrentlySelected == toSelect)
        {
            OnDeselect(eventData);
            Debug.Log("Click Deselected");
        }
        else
        {
            Debug.Log("Selected");
            CurrentlySelected = toSelect;
        }
    }
}
