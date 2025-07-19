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
        public int width;
        [VerticalGroup("MapSettings")]
        public int heigth;

        [VerticalGroup("MapSettings")]
        [Button("Create Map")]
        public void CreateMap()
        {
            Cells = new CellData[width, heigth];
        }
        
        [TableMatrix(DrawElementMethod = "DrawCells", SquareCells = true, HorizontalTitle = "X", VerticalTitle = "Y")]
        public CellData[,] Cells;

        public bool isEditable = false;
        [ShowIf(nameof(isEditable))]
        public EditType editType;
        [ShowIf("@isEditable == true && editType == EditType.BaseTile")]
        public BaseTileType selectedBaseTileType;
        [ShowIf("@isEditable == true && editType == EditType.ElementTile")]
        public TileElementType selectedElementTileType;
        
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

            EditorGUI.DrawRect(rect.Padding(1), renderColor);

            cells[x, y] = currentCell;
            
            return currentCell;
        }


        public enum EditType
        {
            BaseTile = 0,
            ElementTile = 1
        }
        
    }
}