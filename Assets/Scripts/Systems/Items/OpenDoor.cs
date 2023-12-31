using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Animator animator;
    public int keyIndex;
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
    [SerializeField] bool NoKeyRequired;
    public bool isCadavre;

    public void Start()
    {
        animator = GetComponent<Animator>();
        inventoryManager = Inventory.Inventory.Instance;
        mapGrid = MapGrid.Instance;
    }

    public void Opening()
    {

        if (inventoryManager.inventoryData.UseItemByIndex(keyIndex) || NoKeyRequired)
        {
            OpenWaypointBehindDoor();
            OpenWaypointInFrontDoor();
            if (!isCadavre)
            {
                animator.SetTrigger("ChangeState");
            }
            else
            {
                Destroy(gameObject);
            }
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
        MapGrid.Cell cell = mapGrid.GetCell(floorIndexBack, cellBehindDoorX, cellBehindDoorY);
        if (cell != null)
        {
            cell.AllowedMoves |= toAddInBack;
        }
    }

    private void OpenWaypointInFrontDoor()
    {
        MapGrid.Cell cell = mapGrid.GetCell(floorIndexFront, cellInFrontDoorX, cellInFrontDoorY);
        if (cell != null)
        {
            cell.AllowedMoves |= toAddInFront;
        }
    }
}