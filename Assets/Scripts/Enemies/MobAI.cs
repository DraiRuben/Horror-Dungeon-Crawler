using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobAI : MonoBehaviour
{
    protected NavMeshAgent m_agent;
    protected Vector2Int m_gridPos;
    [SerializeField] protected int m_floor;
    [SerializeField] protected int m_attackReach;

    // Start is called before the first frame update
    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_gridPos = MapGrid.Instance.GetClosestCell(m_floor,transform.position);
        MapGrid.Instance.GetCell(m_floor, m_gridPos.x, m_gridPos.y).OccupyingObject = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_floor == PlayerMovement.Instance.CurrentFloor)
        {
            var dist = MapGrid.Instance.DistanceBetweenCells(m_gridPos, PlayerMovement.Instance.GridPos);
            var totalDist = dist.x + dist.y;

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
            }
            else
            {
                m_agent.SetDestination(PlayerMovement.Instance.transform.position);
            }
        }
    }
}
