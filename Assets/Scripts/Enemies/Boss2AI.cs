using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Boss2AI : MobAI
{

    [SerializeField] protected int m_maxRangeTP;
    [SerializeField] protected float m_cooldownTP;
    [SerializeField] protected float m_lastTP;

    public int CurrentFloor;
    protected Vector2Int GridPos;
    private MapGrid.AllowedMovesMask RelativeMoveDir;

    void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_entityStats = GetComponent<EntityStats>();
        StartCoroutine(AttackRoutine());
    }
    // Start is called before the first frame update
    void Start()
    {
        m_gridPos = MapGrid.Instance.GetClosestCell(m_floor, transform.position);
        MapGrid.Instance.GetCell(m_floor, m_gridPos.x, m_gridPos.y).OccupyingObject = gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (m_floor == PlayerMovement.Instance.CurrentFloor)
        {
            var dist = MapGrid.Instance.DistanceBetweenCells(m_gridPos, PlayerMovement.Instance.GridPos);
            var totalDist = dist.x + dist.y;
            transform.rotation = Quaternion.Euler(0, Quaternion.LookRotation(PlayerMovement.Instance.transform.position - transform.position).eulerAngles.y, 0);
            //updates grid pos only if it's not occupied by anything else, also empty previously occupied cell
            var newGridPos = MapGrid.Instance.GetClosestCell(m_floor, transform.position, m_gridPos);
            if (newGridPos != m_gridPos)
            {
                MapGrid.Instance.GetCell(m_floor, m_gridPos.x, m_gridPos.y).OccupyingObject = null;
                var newCell = MapGrid.Instance.GetCell(m_floor, newGridPos.x, newGridPos.y);
                if (newCell.OccupyingObject == null)
                {
                    newCell.OccupyingObject = gameObject;
                }
                m_gridPos = newGridPos;
            }
            // if attack is CQC check if distance to player is <= 1 or if attack is Ranged, check if distance to player <= Reach and both are aligned
            if (m_attackReach <= 1 && totalDist <= 1 || (m_attackReach > 1 && m_attackReach >= totalDist && (dist.x == 0 || dist.y == 0)))
            {
                m_agent.SetDestination(MapGrid.Instance.GetCell(m_floor, m_gridPos.x, m_gridPos.y).Center.position);
                //système d'attaque
                m_isCloseEnough = true;
            }
            else
            {
                m_isCloseEnough = false;
                m_agent.SetDestination(PlayerMovement.Instance.transform.position);
                BossTeleport(totalDist);
            }
        }
    }
    private void BossTeleport(int totalDist)
    {
        if (totalDist < m_maxRangeTP && Time.time - m_lastTP > m_cooldownTP)
        {
            var Dir = MapGrid.Instance.GetRelativeDir(MapGrid.AllowedMovesMask.Bottom, PlayerMovement.Instance.transform.rotation.eulerAngles.y);
            if (MapGrid.Instance.IsValidCell(CurrentFloor, GridPos.x, GridPos.y, Dir))
            {
                m_lastTP = Time.time;
                var Cell = MapGrid.Instance.GetCellInDir(CurrentFloor, GridPos.x, GridPos.y, Dir);
                transform.position = Cell.Center.position;
                return;
            }
            Dir = MapGrid.Instance.GetRelativeDir(MapGrid.AllowedMovesMask.Right, PlayerMovement.Instance.transform.rotation.eulerAngles.y);
            if (MapGrid.Instance.IsValidCell(CurrentFloor, GridPos.x, GridPos.y, Dir))
            {
                m_lastTP = Time.time;
                var Cell = MapGrid.Instance.GetCellInDir(CurrentFloor, GridPos.x, GridPos.y, Dir);
                transform.position = Cell.Center.position;
                return;
            }
            Dir = MapGrid.Instance.GetRelativeDir(MapGrid.AllowedMovesMask.Left, PlayerMovement.Instance.transform.rotation.eulerAngles.y);
            if (MapGrid.Instance.IsValidCell(CurrentFloor, GridPos.x, GridPos.y, Dir))
            {
                m_lastTP = Time.time;
                var Cell = MapGrid.Instance.GetCellInDir(CurrentFloor, GridPos.x, GridPos.y, Dir);
                transform.position = Cell.Center.position;
                return;
            }
            Dir = MapGrid.Instance.GetRelativeDir(MapGrid.AllowedMovesMask.Top, PlayerMovement.Instance.transform.rotation.eulerAngles.y);
            if (MapGrid.Instance.IsValidCell(CurrentFloor, GridPos.x, GridPos.y, Dir))
            {
                m_lastTP = Time.time;
                var Cell = MapGrid.Instance.GetCellInDir(CurrentFloor, GridPos.x, GridPos.y, Dir);
                transform.position = Cell.Center.position;
                return;
            }
        }
    }
}