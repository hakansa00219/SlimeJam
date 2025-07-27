using System;
using Grid;
using Map.Tiles;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "New Level", menuName = "Scriptable/Level")]
    public class GridMap : SerializedScriptableObject
    {
        [VerticalGroup("MapSettings")]
        [SerializeField]
        private int width;
        [VerticalGroup("MapSettings")]
        [SerializeField]
        private int height;

        [VerticalGroup("MapSettings")]
        [Button("Create Map")]
        private void CreateMap()
        {
            Cells = new CellData[width, height];
        }
        
        [TableMatrix(DrawElementMethod = "DrawCells", SquareCells = true, HorizontalTitle = "X", VerticalTitle = "Y")]
        public CellData[,] Cells;
        [SerializeField]
        private (int X, int Y) _startTile;
        [ReadOnly]
        public (int X, int Y) StartTile => (_startTile.X, _startTile.Y);

        [SerializeField] private (int X, int Y) _mainTile;
        [ReadOnly] public (int X, int Y) MainTile => (_mainTile.X, _mainTile.Y);
        [SerializeField]
        private bool isEditable = false;
        [SerializeField]
        [ShowIf(nameof(isEditable))]
        private EditType editType;
        [SerializeField]
        [ShowIf("@isEditable == true && editType == EditType.BaseTile")]
        private BaseTileType selectedBaseTileType;
        [SerializeField]
        [ShowIf("@isEditable == true && editType == EditType.ElementTile")]
        private TileElementType selectedElementTileType;
  
        private static CellData DrawCells(Rect rect, CellData value, int x, int y, GridMap map, CellData[,] cells)
        {
            CellData currentCell = value;
            
            if (Event.current.type == EventType.MouseDown &&
                rect.Contains(Event.current.mousePosition) &&
                map.isEditable)
            {
                if (map.editType == EditType.BaseTile)
                {
                    currentCell.Type = map.selectedBaseTileType;
                    currentCell.ElementType = currentCell.ElementType; // Keep the existing element type
                }
                else if (map.editType == EditType.ElementTile)
                {
                    currentCell.Type = currentCell.Type; // Keep the existing base tile type
                    currentCell.ElementType = map.selectedElementTileType;
                }
                else
                {
                    Debug.LogWarning("Unknown edit type selected.");
                }
                
            }
            
            Color renderColor = currentCell.Type switch 
            {
                BaseTileType.Grass => new Color(0f,0.6f,0f,1f),
                BaseTileType.Water => new Color(0.1f,0.5f,1f,1f),
                BaseTileType.Dirt => new Color(0.7f,0.3f,0.3f, 1f),
                BaseTileType.Sand => new Color(1f,0.7f,0f,1f),
                _ => throw new ArgumentOutOfRangeException()
            };
            string text = currentCell.ElementType switch
            {
                TileElementType.Empty => "O",
                TileElementType.Road => "Road",
                TileElementType.Rock => "Rock",
                TileElementType.Wood => "Wood",
                TileElementType.Metal => "Metal",
                TileElementType.Berry => "Berry",
                TileElementType.Slime => "Slime",
                TileElementType.House => "House",
                TileElementType.Barracks => "Barracks",
                TileElementType.Blacksmith => "Blacksmith",
                TileElementType.Gym => "Gym",
                TileElementType.Warehouse => "Warehouse",
                TileElementType.Flag => "Flag",
                TileElementType.Base => "Base",
                _ => throw new ArgumentOutOfRangeException()
            };
#if UNITY_EDITOR
            EditorGUI.DrawRect(rect.Padding(1), renderColor);
            EditorGUI.TextField(rect.Padding(4), text);
#endif

            cells[x, y] = currentCell;
            
            return currentCell;
        }


        private enum EditType
        {
            BaseTile = 0,
            ElementTile = 1,
        }
        
    }
}