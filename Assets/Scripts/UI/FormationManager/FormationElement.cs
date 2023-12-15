using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FormationElement : MonoBehaviour, IDragHandler, IDropHandler, IBeginDragHandler, IEndDragHandler,IPointerUpHandler
{
    private Image m_image;
    private void Awake()
    {
        m_image = GetComponent<Image>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        MouseFollower.Instance.SetData(m_image.sprite, 1);
        MouseFollower.Instance.Toggle(true);
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        MouseFollower.Instance.Toggle(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        int CurrentIndex = transform.GetSiblingIndex();
        int TargetIndex = eventData.pointerEnter.transform.GetSiblingIndex();

        transform.SetSiblingIndex(TargetIndex);
        eventData.pointerEnter.transform.SetSiblingIndex(CurrentIndex);

        UIPlayerFormation.Instance.UpdatePreviewers();

    }
}
