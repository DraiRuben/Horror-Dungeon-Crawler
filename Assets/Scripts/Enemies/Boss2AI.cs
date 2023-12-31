using UnityEngine;
using UnityEngine.AI;

public class Boss2AI : MobAI
{

    [SerializeField] protected int m_maxRangeTP;
    [SerializeField] protected int m_cooldownTP;



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
            Vector2Int dist = MapGrid.Instance.DistanceBetweenCells(m_gridPos, PlayerMovement.Instance.GridPos);
            int totalDist = dist.x + dist.y;
            transform.rotation = Quaternion.Euler(0, Quaternion.LookRotation(PlayerMovement.Instance.transform.position - transform.position).eulerAngles.y, 0);
            //updates grid pos only if it's not occupied by anything else, also empty previously occupied cell
            Vector2Int newGridPos = MapGrid.Instance.GetClosestCell(m_floor, transform.position, m_gridPos);
            if (newGridPos != m_gridPos)
            {
                MapGrid.Instance.GetCell(m_floor, m_gridPos.x, m_gridPos.y).OccupyingObject = null;
                MapGrid.Cell newCell = MapGrid.Instance.GetCell(m_floor, newGridPos.x, newGridPos.y);
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
                //syst�me d'attaque
                m_isCloseEnough = true;
            }
            else
            {
                m_isCloseEnough = false;
                m_agent.SetDestination(PlayerMovement.Instance.transform.position);
                BossTeleport();
            }
        }
    }
    private void BossTeleport()
    {
        //if()
        // si (la boss est � plus de 2 cases du groupe)
        // elle se tp face au dernier personnage qui lui a fais du d�g�t 

    }
}
