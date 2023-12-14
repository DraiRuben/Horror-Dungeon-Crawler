using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Inventory.UI
{
    public class UIInventoryScript : MonoBehaviour
    {
        [SerializeField] private UIInventoryItem itemPrefab;
        [SerializeField] private RectTransform contentPanel;
        [SerializeField] private UIInventoryDescription itemDescription;
        [SerializeField] private MouseFollower mouseFollower;
        public static UIInventoryScript Instance;

        List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

        private int currentlyDraggedItemIndex = -1;

        public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;

        public event Action<int, int> OnSwapItems;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            ShowHide();
            mouseFollower.Toggle(false);
            itemDescription.ResetDescription();
        }

        public void InitializeInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity,contentPanel);
                listOfUIItems.Add(uiItem);
                uiItem.transform.localPosition = Vector3.zero;
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouseBtnClick += HandleShowItemActions;
            }
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (listOfUIItems.Count > itemIndex)
            {
                listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
                Debug.Log("updatesData");
            }
        }

        private void HandleItemSelection(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            OnDescriptionRequested?.Invoke(index);
        }

        private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            currentlyDraggedItemIndex = index;
            HandleItemSelection(inventoryItemUI);
            OnStartDragging?.Invoke(index);
        }

        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, quantity);
        }

        private void HandleSwap(UIInventoryItem inventoryItemUI)
        {
            if(mouseFollower.isActiveAndEnabled)
            {
                int index = listOfUIItems.IndexOf(inventoryItemUI);
                if (index == -1)
                {
                    return;
                }
                int siblingIndex1 = inventoryItemUI.transform.GetSiblingIndex();
                int siblingIndex2 = listOfUIItems[currentlyDraggedItemIndex].transform.GetSiblingIndex();

                inventoryItemUI.transform.SetSiblingIndex(siblingIndex2);
                listOfUIItems[currentlyDraggedItemIndex].transform.SetSiblingIndex(siblingIndex1);
                HandleItemSelection(inventoryItemUI);
            }
        }

        private void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
        }

        private void HandleEndDrag(UIInventoryItem inventoryItemUI)
        {
            ResetDraggedItem();
        }

        private void HandleShowItemActions(UIInventoryItem inventoryItemUI)
        {

        }

        public void ShowHide()
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
                ResetDraggedItem();
            }
            else
            {
                gameObject.SetActive(true);
                ResetSelection();
            }
        }

        public void ResetSelection()
        {
            itemDescription.ResetDescription();
            DeselectAllItems();
        }

        private void DeselectAllItems()
        {
            foreach (UIInventoryItem item in listOfUIItems)
            {
                item.Deselect();
            }
        }

        public void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
        {
            itemDescription.SetDescription(itemImage, name, description);
            DeselectAllItems();
            listOfUIItems[itemIndex].Select();
        }
    }
}