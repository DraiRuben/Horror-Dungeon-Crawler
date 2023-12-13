using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatsManager : SerializedMonoBehaviour
{
    [TableMatrix(SquareCells = true)]
    public PlayerStats[,] Characters;

    public void TakeDamage(int _damage, GameObject _damageOrigin)
    {
        //gets angle between player looking forward and attack origin
        float AngleY = Quaternion.LookRotation(PlayerMovement.Instance.transform.position - _damageOrigin.transform.position).eulerAngles.y;

        //gets side of the attack's hit, 0 is in front, 1 on the right, 2 in the back, 3 on the left
        Vector3 PlayerLookDir = PlayerMovement.Instance.transform.forward;
        PlayerLookDir.Set(PlayerLookDir.x, 0, PlayerLookDir.z);
        Vector3 OriginLookDir = -_damageOrigin.transform.forward;
        OriginLookDir.Set(OriginLookDir.x, 0, OriginLookDir.z);

        int attackRelativePos = ((int)Mathf.Round(Vector3.Angle(PlayerLookDir, OriginLookDir)/ 90))%4;

        //gets pos in position grid of the two characters in the line directly hit by the attack
        var firstFormationPos = m_formationPositions[attackRelativePos];
        var secondFormationPos = m_formationPositions[(attackRelativePos + 1) % 4];

        //gets first two non null characters in the line on the side of the attack (null means dead)
        List<PlayerStats> CharactersToHit = new List<PlayerStats>()
        { Characters[firstFormationPos.x, firstFormationPos.y],
            Characters[secondFormationPos.x, secondFormationPos.y] }.Where(x => x != null).ToList();
        
        
        //if both the characters directly in line of the attack are dead
        if (CharactersToHit.Count <= 0)
        {
            //choose the 2 in the back instead
            firstFormationPos = m_formationPositions[(attackRelativePos + 2) % 4];
            secondFormationPos = m_formationPositions[(attackRelativePos + 3) % 4];

            CharactersToHit = new List<PlayerStats>()
            { Characters[firstFormationPos.x, firstFormationPos.y],
            Characters[secondFormationPos.x, secondFormationPos.y] }.Where(x => x != null).ToList();
        }
        //choose randomly one character between the ones that can get hit and apply the damage to it
        CharactersToHit[Random.Range(0, CharactersToHit.Count)].TakeDamage(_damage);
    }

    //positions of characters in formation
    private readonly static Vector2Int[] m_formationPositions = 
    {
        new(0,0),
        new(1,0),
        new(1,1),        
        new(0,1),
    };
}
