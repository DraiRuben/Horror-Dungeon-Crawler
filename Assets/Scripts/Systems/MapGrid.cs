using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapGrid : SerializedMonoBehaviour
{

    public static MapGrid Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    [FoldoutGroup("Dungeon")]
    [TableMatrix(SquareCells = true,DrawElementMethod ="DrawElement",HorizontalTitle ="Dungeon Cells")]
    public Cell[,] DungeonCells;

    [FoldoutGroup("Basement")]
    [TableMatrix(SquareCells = true,DrawElementMethod = "DrawElement",HorizontalTitle = "Basement Cells")]
    public Cell[,] BasementCells;

    [FoldoutGroup("Floor 0")]
    [TableMatrix(SquareCells = true, DrawElementMethod = "DrawElement", HorizontalTitle = "Floor 0 Cells")]
    public Cell[,] Floor0Cells;

    [FoldoutGroup("Floor 1")]
    [TableMatrix(SquareCells = true, DrawElementMethod = "DrawElement", HorizontalTitle = "Floor 1 Cells")]
    public Cell[,] Floor1Cells;

    [FoldoutGroup("Attic")]
    [TableMatrix(SquareCells = true, DrawElementMethod = "DrawElement", HorizontalTitle = "Attic Cells")]
    public Cell[,] AtticCells;


    public List<FloorConnection> Connections;

    [Serializable]
    public class Cell
    {
        public Transform Center;
        public AllowedMovesMask AllowedMoves;
    }

    [Serializable]
    public struct FloorConnection
    {
        public Point Pos1;
        public Point Pos2;
        [Serializable]
        public class Point
        {
            public Vector2Int GridPos;
            public int FloorIndex;
        }
    }
    public FloorConnection.Point GetConnectedFloor(Vector2Int _currentGridPos, int _currentFloor)
    {
        foreach(var Connection in Connections)
        {
            if(_currentGridPos == Connection.Pos1.GridPos && _currentFloor == Connection.Pos1.FloorIndex)
            {
                return Connection.Pos2;
            }
            else if (_currentGridPos == Connection.Pos2.GridPos && _currentFloor == Connection.Pos2.FloorIndex)
            {
                return Connection.Pos1;
            }
        }
        return null;
    }
    [System.Flags]
    public enum AllowedMovesMask
    {
        Left = 1 << 1,
        Right = 1 << 2,
        Top = 1 << 3,
        Bottom = 1 << 4,
        All = Left | Right | Top | Bottom
    }

    private List<AllowedMovesMask> Moves = new List<AllowedMovesMask>{ AllowedMovesMask.Top, AllowedMovesMask.Right, AllowedMovesMask.Bottom, AllowedMovesMask.Left };
    private Cell[,] GetFloor(int _floor)
    {
        switch (_floor)
        {
            case 0: return DungeonCells;
            case 1: return BasementCells;
            case 2: return Floor0Cells;
            case 3: return Floor1Cells;
            case 4: return AtticCells;
            default:
                break;
        }
        return null;
    }
    public Vector2Int GetClosestCell(int _floor,Vector2 _worldPos)
    {
        var floor = GetFloor(_floor);

        Vector2Int returnValue = new();
        float closestDistance = 9999999;
        for(int i = 0; i < floor.GetLength(0); i++)
        {
            for(int u = 0;u< floor.GetLength(1);u++)
            {
                if (floor[i,u].Center != null)
                {
                    float Dist = Vector2.Distance(_worldPos, floor[i, u].Center.position);
                    if (Dist < closestDistance)
                    {
                        closestDistance = Dist;
                        returnValue.Set(i, u);
                    }
                }
                
            }
        }
        return returnValue;
    }
    public Vector2Int GetClosestCell(int _floor,Vector3 _worldPos, Vector2Int _gridPos)
    {
        Vector2Int returnValue = new();
        float smallestDist = 99999;
        var floor = GetFloor(_floor);
        //checks all cells around gridpos and gets the one closest to the world pos
        for(int i = 0; i < 3; i++)
        {
            for(int u = 0; u< 3; u++)
            {
                int x = _gridPos.x - 1 + i;
                int y = _gridPos.y - 1 + u;
                if (x>=0 && y>=0 && x<floor.GetLength(0) && y<floor.GetLength(1) && floor[x, y].Center != null)
                {
                    float dist = Vector3.Distance(floor[x, y].Center.position, _worldPos);
                    if (dist < smallestDist)
                    {
                        smallestDist = dist;
                        returnValue.Set(x, y);
                    }
                }
            }
        }
        return returnValue;
    }
    public Vector2Int DistanceBetweenCells(Vector2Int Cell1Pos, Vector2Int Cell2Pos) 
    {
        return new Vector2Int(Mathf.Abs(Cell1Pos.x - Cell2Pos.x), Mathf.Abs(Cell1Pos.y - Cell2Pos.y));
    }
#if UNITY_EDITOR // Editor-related code must be excluded from builds
    [OnInspectorInit]
    private void CreateData()
    {
        if(DungeonCells == null)
        {
            int rows = 25;
            int columns = 25;
            DungeonCells = new Cell[rows, columns];
            BasementCells = new Cell[rows, columns];
            Floor0Cells = new Cell[rows, columns];
            Floor1Cells = new Cell[rows, columns];
            AtticCells = new Cell[rows, columns];

            for(int i =0;i<rows;i++)
            {
                for(int u = 0; u < columns; u++)
                {
                    DungeonCells[i, u] = new();
                    BasementCells[i, u] = new();
                    Floor0Cells[i, u] = new();
                    Floor1Cells[i, u] = new();
                    AtticCells[i, u] = new();
                }
            }
        }
    }
    private static Cell DrawElement(Rect rect, Cell value)
    {
        value.Center = (Transform)SirenixEditorFields.UnityObjectField(rect.VerticalPadding(10), value?.Center, typeof(Transform), true);
        value.AllowedMoves = (AllowedMovesMask)SirenixEditorFields.EnumDropdown(rect, value.AllowedMoves);
        
        return value;
    }
#endif
    public AllowedMovesMask GetRelativeDir(AllowedMovesMask _moveDir, float _rotationY =0)
    {
        return Moves[(Moves.IndexOf(_moveDir) + (int)_rotationY / 90)%4];
    }
    public Cell GetCell(int _floor,int _column, int _row)
    {
        var CurrentFloor = GetFloor(_floor);
        return CurrentFloor[_column, _row];
    }
    public Cell GetCellInDir(int _floor,int _column, int _row, AllowedMovesMask _dir)
    {
        var NextCellPos = GetCellPosInDir(_column,_row, _dir);
        var CurrentFloor = GetFloor(_floor);
        if (NextCellPos.x >= 0 && NextCellPos.y >=0 && NextCellPos.x < CurrentFloor.GetLength(0) && NextCellPos.y < CurrentFloor.GetLength(1))
        {
            return CurrentFloor[NextCellPos.x, NextCellPos.y];
        }
        else return null;
    }
    public Vector2Int GetCellPosInDir(int _column,int _row,  AllowedMovesMask _dir, int _distance =1)
    {
        int NextRow = _row;
        int NextColumn = _column;

        switch (_dir)
        {
            case AllowedMovesMask.Left:
                NextColumn-=_distance;
                break;
            case AllowedMovesMask.Right:
                NextColumn+= _distance;
                break;
            case AllowedMovesMask.Top:
                NextRow-= _distance;
                break;
            case AllowedMovesMask.Bottom:
                NextRow+= _distance;
                break;
        }
        return new Vector2Int(NextColumn,NextRow);
    }
    public bool ValidMovement(int _floor, int _column,int _row,  AllowedMovesMask _relativeMovementDir)
    {
        return (GetFloor(_floor)[_column,_row].AllowedMoves & _relativeMovementDir) == _relativeMovementDir;
    }
}
