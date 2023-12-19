using UnityEngine;
using UnityEngine.UI;

public class FormationPreviewer : MonoBehaviour
{
    public FormationElement m_linkedElement;
    public PlayerStats m_linkedCharacter;
    [SerializeField] private GameObject m_formationMofifier;
    public Image m_image;
    private void Awake()
    {
        m_image = GetComponent<Image>();
    }
    private void Start()
    {
        m_linkedCharacter.OnDeath.AddListener(() => {
            m_image.color = Color.clear;
            m_linkedElement.m_image.color = Color.clear;
        });
    }
    public void UpdateHierarchy()
    {
        int TargetIndex = m_linkedElement.transform.GetSiblingIndex();
        int CurrentIndex = transform.GetSiblingIndex();
        

        transform.parent.GetChild(TargetIndex).SetSiblingIndex(CurrentIndex);
        transform.SetSiblingIndex(TargetIndex);

        (int, int) CurrentGridPos = GridPosToHierarchy[CurrentIndex];
        (int, int) TargetGridPos = GridPosToHierarchy[TargetIndex];

        if (PlayerStatsManager.Instance.Characters[TargetGridPos.Item1, TargetGridPos.Item2] == null
            || PlayerStatsManager.Instance.Characters[TargetGridPos.Item1, TargetGridPos.Item2] != m_linkedCharacter)
        {
            PlayerStatsManager.Instance.Characters[CurrentGridPos.Item1, CurrentGridPos.Item2] = PlayerStatsManager.Instance.Characters[TargetGridPos.Item1, TargetGridPos.Item2];
            PlayerStatsManager.Instance.Characters[TargetGridPos.Item1, TargetGridPos.Item2] = m_linkedCharacter;
        }
    }
    public void ShowHideModifier()
    {
        m_formationMofifier.SetActive(!m_formationMofifier.activeSelf);
    }
    public static readonly (int, int)[] GridPosToHierarchy =
    {
        new (0,0),
        new (1,0),
        new (0,1),
        new (1,1),
    };
}
