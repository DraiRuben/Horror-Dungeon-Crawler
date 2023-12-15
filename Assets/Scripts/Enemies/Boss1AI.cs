using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1AI : MobAI
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_floor == PlayerMovement.Instance.CurrentFloor)
        {
            var dist = MapGrid.Instance.DistanceBetweenCells(m_gridPos, PlayerMovement.Instance.GridPos);
            var totalDist = dist.x + dist.y;

            //if (totalDist > 2) { }

            // if attack is CQC check if distance to player is <= 1 or if attack is Ranged, check if distance to player <= Reach and both are aligned
            if (m_attackReach <= 1 && totalDist <= 1 || (m_attackReach > 1 && m_attackReach >= totalDist && dist.x == 0 || dist.y == 0))
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
