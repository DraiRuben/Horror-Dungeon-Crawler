using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 m_movementInput;
    private float m_rotationInput;

    [SerializeField] private float m_movementFrequency;
    private float m_timeSinceMovement;

    public Vector2Int GridPos;
    public int CurrentFloor;

    private Transform m_transform;
    public static PlayerMovement Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        m_transform = GetComponent<Transform>();
    }
    public void Move(InputAction.CallbackContext _ctx)
    {
        m_movementInput = _ctx.ReadValue<Vector2>();
        
        //if the input has a direction and the player can move
        if(m_movementInput.magnitude > 0 && m_timeSinceMovement>= m_movementFrequency)
        {
            //gets move direction from the camera POV
            var MoveDir = GetMoveDir();
            //converts camera POV input direction into Grid POV input direction
            var RelativeMoveDir = MapGrid.Instance.GetRelativeDir(MoveDir, m_transform.rotation.eulerAngles.y);

            //if the direction we tried to go to is in the list of valid directions in the current cell we have
            if(MapGrid.Instance.ValidMovement(CurrentFloor, GridPos.x, GridPos.y, RelativeMoveDir))
            {
                var NextCell = MapGrid.Instance.GetCellInDir(CurrentFloor, GridPos.x, GridPos.y, RelativeMoveDir);
                //if we have a cell we can go to
                if (NextCell != null && NextCell.Center != null)
                {
                    //if the cell isn't occupied by another entity
                    if(NextCell.OccupyingObject == null)
                    {
                        //get the cell we are on and remove ourselves from occupying it
                        MapGrid.Instance.GetCell(CurrentFloor, GridPos.x, GridPos.y).OccupyingObject = null;
                        //occupy the next cell
                        NextCell.OccupyingObject = gameObject;
                        m_timeSinceMovement = 0;
                        //move to the next cell and add an offset in height since all waypoints are on the ground
                        m_transform.position = NextCell.Center.position;
                        m_transform.position += Vector3.up * 1.5f;
                        UpdateGridPos(RelativeMoveDir);
                    }
                    
                }
                else if (NextCell == null || NextCell.Center == null)
                {
                    var ConnectedFloorInfo = MapGrid.Instance.GetConnectedFloor(GridPos, CurrentFloor);
                    //if we didn't find a next cell in the current floor but the cell we are on is connected to a cell on another floor
                    if (ConnectedFloorInfo != null)
                    {
                        //do the same thing as with any other cell movement
                        var Cell = MapGrid.Instance.GetCell(ConnectedFloorInfo.FloorIndex, ConnectedFloorInfo.GridPos.x, ConnectedFloorInfo.GridPos.y);
                        if(Cell.OccupyingObject == null)
                        {
                            MapGrid.Instance.GetCell(CurrentFloor, GridPos.x, GridPos.y).OccupyingObject = null;
                            Cell.OccupyingObject = gameObject;
                            m_timeSinceMovement = 0;
                            GridPos.Set(ConnectedFloorInfo.GridPos.x, ConnectedFloorInfo.GridPos.y);
                            CurrentFloor = ConnectedFloorInfo.FloorIndex;
                            m_transform.position = MapGrid.Instance.GetCell(CurrentFloor, GridPos.x,GridPos.y).Center.position;
                            m_transform.position += Vector3.up * 1.5f;
                        }

                    }
                }
            }
        }
    }

    public void Rotate(InputAction.CallbackContext _ctx)
    {
        m_rotationInput = _ctx.ReadValue<float>();
        //if we can move
        if(m_timeSinceMovement >= m_movementFrequency)
        {
            //rotate 90° either left or right on the Y axis depending on the input
            if (m_rotationInput > 0)
            {
                m_timeSinceMovement = 0;
                m_transform.rotation = Quaternion.Euler(10, m_transform.rotation.eulerAngles.y +90, 0);
            }
            else if (m_rotationInput < 0)
            {
                m_timeSinceMovement = 0;
                m_transform.rotation = Quaternion.Euler(10, m_transform.rotation.eulerAngles.y - 90, 0);
            }
        }
    }
    public MapGrid.AllowedMovesMask GetMoveDir()
    {
        //translates movment vector input into direction enum
        if (m_movementInput.y > 0)
            return MapGrid.AllowedMovesMask.Top;
        else if (m_movementInput.y < 0)
            return MapGrid.AllowedMovesMask.Bottom;
        else if (m_movementInput.x > 0)
            return MapGrid.AllowedMovesMask.Right;
        else
            return MapGrid.AllowedMovesMask.Left;
    }
    private void UpdateGridPos(MapGrid.AllowedMovesMask relativeMovementDir)
    {
        switch (relativeMovementDir)
        {
            case  MapGrid.AllowedMovesMask.Top:
                GridPos.y--;
                break;
            case  MapGrid.AllowedMovesMask.Right:
                GridPos.x++;
                break;
            case  MapGrid.AllowedMovesMask.Bottom:
                GridPos.y++;
                break;
            case  MapGrid.AllowedMovesMask.Left:
                GridPos.x--;
                break;
        }
    }
    private void Update()
    {
        m_timeSinceMovement += Time.deltaTime;
    }

}
