using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Boss3AI : Boss1AI
{
    [Header("Charge Behaviour Parameters")]

    [SerializeField] protected float m_cooldownCharge;
    [SerializeField] protected int m_maxRangeCharge;

    [SerializeField] protected float chargeSpeed;
    [SerializeField] protected int m_damageCharge;

    [SerializeField] protected float normalSpeed;

    [Space(5)]
    private bool m_isInCharge;

    public int CurrentFloor;
    protected Vector2Int GridPos;
    protected float m_lastCharge;
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
        m_entityStats.OnHealthChanged.AddListener(NextPhaseCheck);
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(transform.position, PlayerMovement.Instance.transform.position - transform.position, out RaycastHit HitInfo, 10f);
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
            }
            MapGrid.Cell newCell = MapGrid.Instance.GetCell(m_floor, newGridPos.x, newGridPos.y);
            if (newCell.OccupyingObject == null)
            {
                newCell.OccupyingObject = gameObject;
            }
            m_gridPos = newGridPos;
            // if attack is CQC check if distance to player is <= 1 or if attack is Ranged, check if distance to player <= Reach and both are aligned
            if (m_attackReach <= 1 && totalDist <= 1 || (m_attackReach > 1 && m_attackReach >= totalDist && (dist.x == 0 || dist.y == 0)))
            {
                if (HitInfo.collider != null && HitInfo.collider.CompareTag("Player"))
                    m_agent.SetDestination(MapGrid.Instance.GetCell(m_floor, m_gridPos.x, m_gridPos.y).Center.position);
                //système d'attaque
                m_isCloseEnough = true;
                if (m_isInCharge)
                {
                    m_isInCharge = false;
                    MapGrid.AllowedMovesMask AttackDir = MapGrid.Instance.GetRelativeDir(MapGrid.AllowedMovesMask.Top, transform.rotation.eulerAngles.y);
                    AttackSystem.Instance.CQCAttack(m_gridPos, m_floor, AttackDir, m_damageCharge, gameObject);
                }
            }
            else
            {
                m_isCloseEnough = false;
                if (Time.time - m_previousDestinationSetTime > m_destinationUpdateFrequency
                    && HitInfo.collider != null && HitInfo.collider.CompareTag("Player"))
                {
                    m_previousDestinationSetTime = Time.time;

                    m_agent.SetDestination(PlayerMovement.Instance.transform.position);
                }
                TryCharge(totalDist);
            }
        }

    }

    protected override IEnumerator AttackRoutine()
    {
        float timeSincePreviousAttack = 0;
        while (true)
        {
            yield return new WaitUntil(() => m_isCloseEnough);
            if (timeSincePreviousAttack > (10f / m_entityStats.Dexterity))
            {
                timeSincePreviousAttack = 0;
                MapGrid.AllowedMovesMask AttackDir = MapGrid.Instance.GetRelativeDir(MapGrid.AllowedMovesMask.Top, transform.rotation.eulerAngles.y);
                if (!m_projectileAttacks)
                {

                    AttackSystem.Instance.CQCAttack(m_gridPos, m_floor, AttackDir, m_entityStats.Strength, gameObject);
                }
                else
                {
                    AttackSystem.Instance.RangedAttack(m_gridPos, m_floor, AttackDir, m_entityStats.Strength, m_attackReach, m_entityStats.Dexterity, gameObject);
                }
            }

            timeSincePreviousAttack += Time.deltaTime;
            yield return null;
        }
    }

    private void TryCharge(int totalDist)
    {
        if (totalDist <= m_maxRangeCharge && Time.time - m_lastCharge > m_cooldownCharge)
        {
            GetComponent<NavMeshAgent>().speed = chargeSpeed;
            m_isInCharge = true;
            m_lastCharge = Time.time;
            StartCoroutine(ChargeBehaviour());
        }
    }

    private IEnumerator ChargeBehaviour()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<NavMeshAgent>().speed = normalSpeed;
        m_isInCharge = false;
    }
}
