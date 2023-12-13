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
    public bool InflictDamageAtGridPos(Vector2Int _gridAttackPos, int _floor, int _damage)
    {
        var OccupyingEntity = MapGrid.Instance.GetCell(_floor,_gridAttackPos.x,_gridAttackPos.y)?.OccupyingObject;

        if(OccupyingEntity != null)
        {
            var PlayerStatsManager = OccupyingEntity.GetComponent<PlayerStatsManager>();
            if(PlayerStatsManager != null )
            {
                return true;
            }
            var EntityStats = OccupyingEntity.GetComponent<EntityStats>();
            if(EntityStats != null )
            {
                return true;
            }
        }
        return false;
        
    }
    public void CQCAttack(Vector2Int _gridAttackPos, int _floor, MapGrid.AllowedMovesMask _attackDir, int _damage)
    {
        //if there's no wall, inflict damage one cell in front
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
        int currentCellDistance = 1;
        float timer=0f;
        //if the projectile wasn't shot at a wall
        if (MapGrid.Instance.ValidMovement(_floor, currentGridPos.x, currentGridPos.y, _attackDir))
        while (currentCellDistance < _range) 
        {
            //if we dealt the damage, stop dealing anymore damage
            if(InflictDamageAtGridPos(currentGridPos, _floor, _damage))
                    yield break;

            //move projectile position each 1/projectileSpeed seconds
            if (timer > (1f / _projectileSpeed))
            {
                timer = 0;
                currentCellDistance++;
                //if the projectile won't hit a wall then update the position, else stop the projectile entirely
                if (MapGrid.Instance.ValidMovement(_floor, currentGridPos.x, currentGridPos.y, _attackDir))
                {
                    currentGridPos = MapGrid.Instance.GetCellPosInDir(currentGridPos.x, currentGridPos.y, _attackDir);
                }
                else yield break;
            }
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
