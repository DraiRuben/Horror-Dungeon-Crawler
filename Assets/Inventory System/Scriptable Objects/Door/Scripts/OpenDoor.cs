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
    private MapGrid mapGrid;
    public bool isOpen;
    [field: SerializeField] int cellBehindDoorX;
    [field: SerializeField] int cellBehindDoorY;
    [field: SerializeField] int floorIndex;

    public void Start()
    {
        isOpen = false;
        inventoryManager = InventoryManager.Instance;
        mapGrid = MapGrid.Instance;
    }

    public void Opening()
    {
        if (inventoryManager.inventoryData.UseItemByIndex(keyIndex))
        {
            isOpen = true;
            BlockWaypointBehindDoor();
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

    private void BlockWaypointBehindDoor()
    {
        var cell = mapGrid.GetCell(floorIndex, cellBehindDoorX, cellBehindDoorY);
        if (cell != null)
        {
            if (!isOpen)
            {
                Debug.Log("Blocking movements behind the door.");
                cell.AllowedMoves &= MapGrid.AllowedMovesMask.Left;
                cell.AllowedMoves &= MapGrid.AllowedMovesMask.Right;
                cell.AllowedMoves &= MapGrid.AllowedMovesMask.Top;
                cell.AllowedMoves &= MapGrid.AllowedMovesMask.Bottom;
            }
            else
            {
                Debug.Log("Opening movements behind the door.");
                cell.AllowedMoves = MapGrid.AllowedMovesMask.All;
            }
        }
    }
}