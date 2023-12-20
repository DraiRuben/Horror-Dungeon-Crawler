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
    private bool IsHovering;
    public override void OnDeselect(BaseEventData eventData)
    {
        if (EventSystem.current.currentSelectedGameObject == null
            || (!EventSystem.current.currentSelectedGameObject.CompareTag("HealingItems") && EventSystem.current.currentSelectedGameObject != gameObject)
            || !IsHovering)
        {
            base.OnDeselect(eventData);
            if (CurrentlySelected == toSelect)
            {
                CurrentlySelected = null;
            }
        }
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        IsHovering = true;
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        IsHovering = false;
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (CurrentlySelected == toSelect)
        {
            CurrentlySelected = null;
            base.OnDeselect(eventData);
            EventSystem.current.SetSelectedGameObject(null);
            Debug.Log("Click Deselected");
        }
        else
        {
            Debug.Log("Selected");
            CurrentlySelected = toSelect;
        }
    }
}
