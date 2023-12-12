using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : SerializedMonoBehaviour
{
    [TableMatrix(SquareCells = true)]
    public PlayerStats[,] Characters;

    public void TakeDamage(int _damage, GameObject _damageOrigin)
    {
        float AngleY = Quaternion.LookRotation(PlayerMovement.Instance.transform.position - _damageOrigin.transform.position).eulerAngles.y;
        int attackRelativePos = (int)AngleY % 90;
        int randChar = UnityEngine.Random.Range(0, 2);

        var formationPos = m_formationPositions[attackRelativePos];
        if (Characters[formationPos.x, formationPos.y] != null) { }
    }
    private readonly static List<Vector2Int> m_formationPositions = new List<Vector2Int>() 
    {
        new Vector2Int(0,0),
        new Vector2Int(1,0),
        new Vector2Int(0,1),
        new Vector2Int(1,1),
    };
}
