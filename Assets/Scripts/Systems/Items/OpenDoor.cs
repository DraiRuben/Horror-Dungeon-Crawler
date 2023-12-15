using Inventory;
using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [field: SerializeField] public int keyIndex;
    private Inventory.Inventory inventoryManager;
    private MapGrid mapGrid;
    [field: SerializeField] MapGrid.AllowedMovesMask toAddInBack;
    [field: SerializeField] int cellBehindDoorX;
    [field: SerializeField] int cellBehindDoorY;
    [field: SerializeField] int floorIndexBack;
    [field: SerializeField] MapGrid.AllowedMovesMask toAddInFront;
    [field: SerializeField] int cellInFrontDoorX;
    [field: SerializeField] int cellInFrontDoorY;
    [field: SerializeField] int floorIndexFront;

    public void Start()
    {
        inventoryManager = Inventory.Inventory.Instance;
        mapGrid = MapGrid.Instance;
    }

    public void Opening()
    {
        if (inventoryManager.inventoryData.UseItemByIndex(keyIndex))
        {
            OpenWaypointBehindDoor();
            OpenWaypointInFrontDoor();
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

    private void OpenWaypointBehindDoor()
    {
        var cell = mapGrid.GetCell(floorIndexBack, cellBehindDoorX, cellBehindDoorY);
        if (cell != null)
        {
            cell.AllowedMoves |= toAddInBack;
        }
    }

    private void OpenWaypointInFrontDoor()
    {
        var cell = mapGrid.GetCell(floorIndexFront, cellInFrontDoorX, cellInFrontDoorY);
        if (cell != null)
        {
            cell.AllowedMoves |= toAddInFront;
        }
    }
}