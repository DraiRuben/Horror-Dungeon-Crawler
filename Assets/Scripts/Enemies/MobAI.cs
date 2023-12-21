using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MobAI : MonoBehaviour
{
    protected NavMeshAgent m_agent;
    protected Vector2Int m_gridPos;
    [Header("Base Parameters")]
    [SerializeField] protected int m_floor;
    [SerializeField] protected int m_attackReach;
    [SerializeField] protected bool m_projectileAttacks;
    [SerializeField] protected float m_destinationUpdateFrequency;
    [Space(5)]

    protected bool m_isCloseEnough;
    protected bool m_hasFoundPlayer;
    protected EntityStats m_entityStats;

    protected float m_previousDestinationSetTime;

    private AudioManagerEnnemies m_audioManager;
    // Start is called before the first frame update
    void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_entityStats = GetComponent<EntityStats>();
        m_audioManager = GetComponentInChildren<AudioManagerEnnemies>();
        StartCoroutine(AttackRoutine());
    }
    private void Start()
    {
        m_gridPos = MapGrid.Instance.GetClosestCell(m_floor, transform.position);
        MapGrid.Instance.GetCell(m_floor, m_gridPos.x, m_gridPos.y).OccupyingObject = gameObject;
        StartCoroutine(TryFindPlayer());
    }
    protected IEnumerator TryFindPlayer()
    {
        while (!m_hasFoundPlayer)
        {
            Physics.Raycast(transform.position, PlayerMovement.Instance.transform.position - transform.position, out RaycastHit HitInfo, 10f);
            if (HitInfo.collider != null && HitInfo.collider.CompareTag("Player"))
            {
                m_hasFoundPlayer = true;
            }
            yield return null;
        }
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
                //syst�me d'attaque
                m_isCloseEnough = true;

            }
            else
            {
                m_isCloseEnough = false;
                if (Time.time - m_previousDestinationSetTime > m_destinationUpdateFrequency)
                {
                    m_previousDestinationSetTime = Time.time;

                    m_agent.SetDestination(PlayerMovement.Instance.transform.position);
                }
            }
        }
    }

    protected virtual IEnumerator AttackRoutine()
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
                PlayAttackSFX();
            }

            timeSincePreviousAttack += Time.deltaTime;
            yield return null;
        }
    }
    protected virtual void PlayAttackSFX()
    {
        m_audioManager?.PlaySFXMob(m_audioManager.Dog_Attack);
    }
}
