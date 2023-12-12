using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    public class MapItem : MonoBehaviour
    {
        public GameObject itemPrefab;
        public Transform location;
        public Item itemSO;

        private int itemQuantity = 1;


        public void PickUpItemOnMap()
        {
            UIInventoryScript.Instance.UpdateData(itemSO.index, itemSO.ItemImage, itemQuantity);
            InventoryManager.Instance.UpdateData();
            Destroy(gameObject);
        }

        private void OnMouseDown()
        {
            PickUpItemOnMap();
        }
    }
}