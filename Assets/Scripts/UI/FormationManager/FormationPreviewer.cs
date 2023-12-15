using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationPreviewer : MonoBehaviour
{
    [SerializeField] private FormationElement m_linkedElement;
    public PlayerStats m_linkedCharacter;
    public void UpdateHierarchy()
    {
        int TargetIndex = m_linkedElement.transform.GetSiblingIndex();
        int CurrentIndex = transform.GetSiblingIndex();

        transform.parent.GetChild(TargetIndex).SetSiblingIndex(CurrentIndex);
        transform.SetSiblingIndex(TargetIndex);

        var CurrentGridPos = GridPosToHierarchy[CurrentIndex];
        var TargetGridPos = GridPosToHierarchy[TargetIndex];

        if (PlayerStatsManager.Instance.Characters[TargetGridPos.Item1,TargetGridPos.Item2]==null 
            || PlayerStatsManager.Instance.Characters[TargetGridPos.Item1, TargetGridPos.Item2] != m_linkedCharacter)
        {
            PlayerStatsManager.Instance.Characters[CurrentGridPos.Item1, CurrentGridPos.Item2] = PlayerStatsManager.Instance.Characters[TargetGridPos.Item1, TargetGridPos.Item2];
            PlayerStatsManager.Instance.Characters[TargetGridPos.Item1, TargetGridPos.Item2] = m_linkedCharacter;
        }
    }
    public static readonly (int, int)[] GridPosToHierarchy =
    {
        new (0,0),
        new (1,0),
        new (0,1),
        new (1,1),
    };
}
