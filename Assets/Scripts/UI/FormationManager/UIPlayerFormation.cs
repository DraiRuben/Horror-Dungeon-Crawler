using System.Collections.Generic;
using UnityEngine;

public class UIPlayerFormation : MonoBehaviour
{
    [SerializeField] private PlayerStatsManager m_playerStatsManager;
    public List<FormationPreviewer> Previewers;
    public static UIPlayerFormation Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void UpdatePreviewers()
    {
        foreach (FormationPreviewer previewer in Previewers)
        {
            previewer.UpdateHierarchy();
        }
    }

}
