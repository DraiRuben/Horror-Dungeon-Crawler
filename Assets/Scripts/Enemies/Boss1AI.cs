using UnityEngine;
using UnityEngine.AI;

public class Boss1AI : MobAI
{
    [Header("Phase 2 Behaviour Parameters")]
    [SerializeField] protected int NextPhaseHP;
    [SerializeField] protected int NextPhaseStrength;
    [SerializeField] protected int NextPhaseDexterity;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Sprite NextPhaseSprite;

    private AudioManagerFather m_audioManager;
    public bool father_Phase2 = false;

    void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_entityStats = GetComponent<EntityStats>();
        m_audioManager = GetComponentInChildren<AudioManagerFather>();
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
    protected override void PlayAttackSFX()
    {
        m_audioManager.PlaySFXFather(m_audioManager.Father_Attack);
    }

    protected void NextPhaseCheck()
    {
        if (m_entityStats.CurrentHealth < NextPhaseHP)
        {
            m_entityStats.Dexterity = NextPhaseDexterity;
            m_entityStats.Strength = NextPhaseStrength;
            m_entityStats.OnHealthChanged.RemoveListener(NextPhaseCheck);
            father_Phase2 = true;
            spriteRenderer.sprite = NextPhaseSprite;
        }
    }
}
