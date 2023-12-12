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
        Instance = this;
        m_transform = GetComponent<Transform>();
    }
    public void Move(InputAction.CallbackContext _ctx)
    {
        m_movementInput = _ctx.ReadValue<Vector2>();
        
        if(m_movementInput.magnitude > 0 && m_timeSinceMovement>= m_movementFrequency)
        {
            
            var MoveDir = GetMoveDir();
            var RelativeMoveDir = MapGrid.Instance.GetRelativeDir(MoveDir, m_transform.rotation.eulerAngles.y);
            if(MapGrid.Instance.ValidMovement(CurrentFloor, GridPos.x, GridPos.y, RelativeMoveDir))
            {
                var NextCell = MapGrid.Instance.GetCellInDir(CurrentFloor, GridPos.x, GridPos.y, RelativeMoveDir);
                if (NextCell != null && NextCell.Center != null)
                {
                    if(NextCell.OccupyingObject == null)
                    {
                        MapGrid.Instance.GetCell(CurrentFloor, GridPos.x, GridPos.y).OccupyingObject = null;
                        NextCell.OccupyingObject = gameObject;
                        m_timeSinceMovement = 0;
                        m_transform.position = NextCell.Center.position;
                        m_transform.position += Vector3.up * 1.5f;
                        UpdateGridPos();
                    }
                    
                }
                else if (NextCell == null || NextCell.Center == null)
                {
                    var ConnectedFloorInfo = MapGrid.Instance.GetConnectedFloor(GridPos, CurrentFloor);
                    if (ConnectedFloorInfo != null)
                    {
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
        if(m_timeSinceMovement >= m_movementFrequency)
        {
            if (m_rotationInput > 0)
            {
                m_timeSinceMovement = 0;
                m_transform.rotation = Quaternion.Euler(0, m_transform.rotation.eulerAngles.y +90, 0);
            }
            else if (m_rotationInput < 0)
            {
                m_timeSinceMovement = 0;
                m_transform.rotation = Quaternion.Euler(0, m_transform.rotation.eulerAngles.y - 90, 0);
            }
        }
    }
    private MapGrid.AllowedMovesMask GetMoveDir()
    {
        if (m_movementInput.y > 0)
            return MapGrid.AllowedMovesMask.Top;
        else if (m_movementInput.y < 0)
            return MapGrid.AllowedMovesMask.Bottom;
        else if (m_movementInput.x > 0)
            return MapGrid.AllowedMovesMask.Right;
        else
            return MapGrid.AllowedMovesMask.Left;
    }
    private void UpdateGridPos()
    {
        int MovementInputChange;
        if (m_movementInput.y > 0)
            MovementInputChange=0;
        else if (m_movementInput.y < 0)
            MovementInputChange=2;
        else if (m_movementInput.x > 0)
            MovementInputChange=1;
        else
            MovementInputChange=3;

        int RotationInputChange = (int)m_transform.rotation.eulerAngles.y / 90;

        int ResultChange = (MovementInputChange + RotationInputChange )%4;

        switch (ResultChange)
        {
            case 0:
                GridPos.y--;
                break;
            case 1:
                GridPos.x++;
                break;
            case 2:
                GridPos.y++;
                break;
            case 3:
                GridPos.x--;
                break;
        }
    }
    private void Update()
    {
        m_timeSinceMovement += Time.deltaTime;
    }

}
