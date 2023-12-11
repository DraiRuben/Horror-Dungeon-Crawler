using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public static AttackSystem Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void CQCAttack(Vector2Int _gridAttackPos, int _floor, MapGrid.AllowedMovesMask _attackDir, int _damage)
    {

    }
}
