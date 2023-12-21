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

    protected float m_lastCharge;

    private AudioManagerBaby m_audioManager;
    void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_entityStats = GetComponent<EntityStats>();
        m_audioManager = GetComponentInChildren<AudioManagerBaby>();
        StartCoroutine(AttackRoutine());

    }
    // Start is called before the first frame update
    void Start()
    {
        m_gridPos = MapGrid.Instance.GetClosestCell(m_floor, transform.position);
        MapGrid.Instance.GetCell(m_floor, m_gridPos.x, m_gridPos.y).OccupyingObject = gameObject;
        m_entityStats.OnHealthChanged.AddListener(NextPhaseCheck);
        StartCoroutine(TryFindPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        if (m_floor == PlayerMovement.Instance.CurrentFloor
            && m_hasFoundPlayer)
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
                if (Time.time - m_previousDestinationSetTime > m_destinationUpdateFrequency)
                {
                    m_previousDestinationSetTime = Time.time;

                    m_agent.SetDestination(PlayerMovement.Instance.transform.position);
                }
                TryCharge(totalDist);
            }
        }

    }

    protected override void PlayAttackSFX()
    {
        m_audioManager.PlaySFXBaby(m_audioManager.Baby_Attack);
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
