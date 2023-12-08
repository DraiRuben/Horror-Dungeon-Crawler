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

    [Serializable]
    public class Cell
    {
        public Transform Center;
        public AllowedMovesMask AllowedMoves;
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
    private Cell[,] GetFloor(int level)
    {
        switch(level)
        {
            case 0:return DungeonCells;
            case 1: return BasementCells;
            case 2: return Floor0Cells;
            case 3: return Floor1Cells;
            case 4: return AtticCells;
        }
        return null;
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
        value.Center = (Transform)SirenixEditorFields.UnityObjectField(rect.VerticalPadding(10), value == null ? null : value.Center, typeof(Transform), true);
        value.AllowedMoves = (AllowedMovesMask)SirenixEditorFields.EnumDropdown(rect, value.AllowedMoves);
        
        return value;
    }
#endif
    public AllowedMovesMask GetRelativeDir(AllowedMovesMask _moveDir, float _rotationY =0)
    {
        return Moves[(Moves.IndexOf(_moveDir) + (int)_rotationY / 90)%4];
    }
    public Cell GetCellInDir(int _floor, int _row, int _column, AllowedMovesMask _dir)
    {
        int NextRow = _row;
        int NextColumn = _column;

        switch (_dir)
        {
            case AllowedMovesMask.Left:
                NextColumn--;
                break;
            case AllowedMovesMask.Right:
                NextColumn++;
                break;
            case AllowedMovesMask.Top:
                NextRow--;
                break;
            case AllowedMovesMask.Bottom:
                NextRow++;
                break;
        }
        var CurrentFloor = GetFloor(_floor);
        if (NextRow >=0 && NextColumn >=0 && NextRow < CurrentFloor.GetLength(1) && NextColumn < CurrentFloor.GetLength(0))
        {
            return CurrentFloor[NextColumn, NextRow];
        }
        else return null;
    }
    public bool ValidMovement(int _floor, int _row, int _column, AllowedMovesMask _relativeMovementDir)
    {
        return (GetFloor(_floor)[_column,_row].AllowedMoves & _relativeMovementDir) == _relativeMovementDir;
    }
}
