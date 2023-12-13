using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        var firstFormationPos = m_formationPositions[attackRelativePos];
        var secondFormationPos = m_formationPositions[(attackRelativePos+1)%4];

        //gets first two non null characters in the line on the side of the attack
        List<PlayerStats> CharactersToHit = new List<PlayerStats>()
        { Characters[firstFormationPos.x, firstFormationPos.y],
            Characters[secondFormationPos.x, secondFormationPos.y] }.Where(x => x != null).ToList();
        
        if (CharactersToHit.Count<=0)
        {
            //if both the characters directly in line of the attack are dead, choose the 2 in the back instead
            firstFormationPos = m_formationPositions[(attackRelativePos + 2) % 4];
            secondFormationPos = m_formationPositions[(attackRelativePos + 3) % 4];

            CharactersToHit = new List<PlayerStats>()
            { Characters[firstFormationPos.x, firstFormationPos.y],
            Characters[secondFormationPos.x, secondFormationPos.y] }.Where(x => x != null).ToList();
        }
        CharactersToHit[Random.Range(0, CharactersToHit.Count)].TakeDamage(_damage); 
    }
    private readonly static List<Vector2Int> m_formationPositions = new List<Vector2Int>() 
    {
        new Vector2Int(0,0),
        new Vector2Int(1,0),
        new Vector2Int(0,1),
        new Vector2Int(1,1),
    };
}
