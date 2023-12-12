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
    public void InflictDamageAtGridPos(Vector2Int _gridAttackPos, int _floor, int _damage)
    {
        var OccupyingEntity = MapGrid.Instance.GetCell(_floor,_gridAttackPos.x,_gridAttackPos.y)?.OccupyingObject;

        if(OccupyingEntity != null)
        {
            var PlayerStatsManager = OccupyingEntity.GetComponent<PlayerStatsManager>();
            if(PlayerStatsManager != null )
            {
                return;
            }
            var EntityStats = OccupyingEntity.GetComponent<EntityStats>();
            if(EntityStats != null )
            {
                return;
            }
        }
        
    }
    public void CQCAttack(Vector2Int _gridAttackPos, int _floor, MapGrid.AllowedMovesMask _attackDir, int _damage)
    {
        //if there's no wall
        if (MapGrid.Instance.ValidMovement(_floor, _gridAttackPos.x, _gridAttackPos.y, _attackDir))
        {
            var DamagedCellPos = MapGrid.Instance.GetCellPosInDir(_gridAttackPos.x, _gridAttackPos.y, _attackDir);
            InflictDamageAtGridPos(DamagedCellPos, _floor, _damage);
        }
    }

    public void RangedAttack(Vector2Int _gridAttackPos, int _floor, MapGrid.AllowedMovesMask _attackDir, int _damage,int _range, float _projectileSpeed)
    {
        StartCoroutine(RangedAttackBehaviour(_gridAttackPos,_floor,_attackDir,_damage,_range,_projectileSpeed));
    }
    private IEnumerator RangedAttackBehaviour(Vector2Int _gridAttackPos, int _floor, MapGrid.AllowedMovesMask _attackDir, int _damage, int _range, float _projectileSpeed)
    {
        Vector2Int currentGridPos = _gridAttackPos;
        for(int i = 0; i < _range; i++)
        {
            if (MapGrid.Instance.ValidMovement(_floor, currentGridPos.x, currentGridPos.y, _attackDir))
            {
                currentGridPos = MapGrid.Instance.GetCellPosInDir(currentGridPos.x, currentGridPos.y, _attackDir);
                InflictDamageAtGridPos(currentGridPos, _floor, _damage);
            }
            else
                yield break;

            yield return new WaitForSeconds(1f/_projectileSpeed);
        }
    }
}
