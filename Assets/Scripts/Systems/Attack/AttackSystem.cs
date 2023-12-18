using System.Collections;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public static AttackSystem Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public bool InflictDamageAtGridPos(Vector2Int _gridAttackPos, int _floor, int _damage, GameObject _origin = null)
    {
        GameObject OccupyingEntity = MapGrid.Instance.GetCell(_floor, _gridAttackPos.x, _gridAttackPos.y)?.OccupyingObject;

        if (OccupyingEntity != null)
        {
            PlayerStatsManager PlayerStatsManager = OccupyingEntity.GetComponent<PlayerStatsManager>();
            if (PlayerStatsManager != null)
            {
                PlayerStatsManager.TakeDamage(_damage, _origin);
                return true;
            }
            EntityStats EntityStats = OccupyingEntity.GetComponent<EntityStats>();
            if (EntityStats != null)
            {
                EntityStats.TakeDamage(_damage);
                return true;
            }
        }
        return false;

    }
    public void CQCAttack(Vector2Int _gridAttackPos, int _floor, MapGrid.AllowedMovesMask _attackDir, int _damage, GameObject _origin = null)
    {
        //if there's no wall, inflict damage one cell in front
        if (MapGrid.Instance.ValidMovement(_floor, _gridAttackPos.x, _gridAttackPos.y, _attackDir))
        {
            Vector2Int DamagedCellPos = MapGrid.Instance.GetCellPosInDir(_gridAttackPos.x, _gridAttackPos.y, _attackDir);
            InflictDamageAtGridPos(DamagedCellPos, _floor, _damage, _origin);
        }
    }

    public void RangedAttack(Vector2Int _gridAttackPos, int _floor, MapGrid.AllowedMovesMask _attackDir, int _damage, int _range, float _projectileSpeed, GameObject _origin = null)
    {
        //if the projectile wasn't shot at a wall
        MapGrid.Cell nextCell = MapGrid.Instance.GetCellInDir(_floor, _gridAttackPos.x, _gridAttackPos.y, _attackDir);
        if (MapGrid.Instance.ValidMovement(_floor, _gridAttackPos.x, _gridAttackPos.y, _attackDir)
            && nextCell != null && nextCell.Center != null)
            StartCoroutine(RangedAttackBehaviour(_gridAttackPos, _floor, _attackDir, _damage, _range, _projectileSpeed, _origin));
    }
    private IEnumerator RangedAttackBehaviour(Vector2Int _gridAttackPos, int _floor, MapGrid.AllowedMovesMask _attackDir, int _damage, int _range, float _projectileSpeed, GameObject _origin)
    {
        Vector2Int currentGridPos = MapGrid.Instance.GetCellPosInDir(_gridAttackPos.x, _gridAttackPos.y, _attackDir);
        int currentCellDistance = 1;
        float timer = 0f;

        while (currentCellDistance < _range)
        {
            //if we dealt the damage, stop dealing anymore damage
            if (InflictDamageAtGridPos(currentGridPos, _floor, _damage, _origin))
                yield break;

            //move projectile position each 1/projectileSpeed seconds
            if (timer > (1f / _projectileSpeed))
            {
                timer = 0;
                currentCellDistance++;
                MapGrid.Cell nextCell = MapGrid.Instance.GetCellInDir(_floor, _gridAttackPos.x, _gridAttackPos.y, _attackDir);
                //if the projectile won't hit a wall then update the position, else stop the projectile entirely
                if (MapGrid.Instance.ValidMovement(_floor, currentGridPos.x, currentGridPos.y, _attackDir)
                    && nextCell != null && nextCell.Center != null)
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
