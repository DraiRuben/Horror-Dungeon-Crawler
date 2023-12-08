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

    [SerializeField] private int m_currentRow;
    [SerializeField] private int m_currentColumn;
    [SerializeField] private int m_currentFloor;

    private Transform m_transform;
    private void Awake()
    {
        m_transform = GetComponent<Transform>();
    }
    public void Move(InputAction.CallbackContext _ctx)
    {
        m_movementInput = _ctx.ReadValue<Vector2>();
        
        if(m_movementInput.magnitude > 0 && m_timeSinceMovement>= m_movementFrequency)
        {
            m_timeSinceMovement = 0;
            var MoveDir = GetMoveDir();
            var RelativeMoveDir = MapGrid.Instance.GetRelativeDir(MoveDir, m_transform.rotation.eulerAngles.y);
            var NextCell = MapGrid.Instance.GetCellInDir(m_currentFloor, m_currentRow, m_currentColumn, RelativeMoveDir);
            if (NextCell != null && NextCell.Center !=null && MapGrid.Instance.ValidMovement(m_currentFloor, m_currentRow, m_currentColumn, RelativeMoveDir)) 
            {
                m_transform.position = NextCell.Center.position;
                UpdateGridPos();
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
                m_currentRow--;
                break;
            case 1:
                m_currentColumn++;
                break;
            case 2:
                m_currentRow++;
                break;
            case 3:
                m_currentColumn--;
                break;
        }
    }
    private void Update()
    {
        m_timeSinceMovement += Time.deltaTime;
    }

}
