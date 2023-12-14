using Inventory;
using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [field: SerializeField] public int keyIndex;
    private InventoryManager inventoryManager;
    private PlayerMovement playerMovement;

    public void Start()
    {
        inventoryManager = InventoryManager.Instance;
        playerMovement = PlayerMovement.Instance;
    }

    public void Opening()
    {
        if (inventoryManager.inventoryData.UseItemByIndex(keyIndex))
        {
            
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("No Key");
        }
    }

    public void OnMouseDown()
    {
        Opening();
    }
}
