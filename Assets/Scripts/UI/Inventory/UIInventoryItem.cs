using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        public Image itemImage;
        [SerializeField] private TMP_Text quantityTxt;
        [SerializeField] private Image borderImage;
        [SerializeField] private bool m_isPlaceholder;
        public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;

        private bool empty = true;

        public void Awake()
        {
            itemImage.gameObject.SetActive(!empty);
            Deselect();
        }

        public void Deselect()
        {
            if (borderImage != null) borderImage.enabled = false;
        }

        public void SetData(Sprite sprite, int quantity)
        {
            itemImage.sprite = sprite;
            Debug.Log(quantity > 0);
            if (quantityTxt != null) quantityTxt.text = quantity + "";
            empty = quantity <= 0;
            itemImage.gameObject.SetActive(!empty);
        }

        public void Select()
        {
            if (borderImage != null) borderImage.enabled = true;
        }

        public void OnPointerClick(PointerEventData pointerData)
        {
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseBtnClick?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (empty)
                return;
            OnItemBeginDrag?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }
        public void OnDrag(PointerEventData eventData)
        {

        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!m_isPlaceholder)
                OnItemDroppedOn?.Invoke(eventData.pointerEnter.GetComponent<UIInventoryItem>());
        }

        public void OnDrop(PointerEventData eventData)
        {

        }

        public void OnPointerDown(PointerEventData eventData)
        {

        }
    }
}