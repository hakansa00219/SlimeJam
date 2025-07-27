using System;
using Entity;
using Map.Tiles;
using Scriptable;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Utility;

namespace Grid
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GridMap level1;
        [SerializeField] private Tilemap baseTilemap;
        [SerializeField] private Tilemap overlayTilemap;
        [SerializeField] private TileTextures tileTextures;
        [SerializeField] private Entities entities;
        [SerializeField] private EntitySpawner entitySpawner;
        
        private CellData[,] _cells;
        private int _width;
        private int _height;
        
        
        private void Awake()
        {
            _cells = level1.Cells;
            _width = _cells.GetLength(0);
            _height = _cells.GetLength(1);
            
            CreateTilemaps();
            RenderTilemaps();
            CreateEntities();
        }
        
        private void CreateTilemaps()
        {
            baseTilemap.ClearAllTiles();
            baseTilemap.size = new Vector3Int(_width, _height);
            
            overlayTilemap.ClearAllTiles();
            overlayTilemap.size = new Vector3Int(_width, _height);
        }
        private void RenderTilemaps()
        {
            RenderBaseTilemap();
            RenderOverlayTilemap();
        }
        private void RenderBaseTilemap()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = _height - 1; y >= 0; y--)
                {
                    Tile tile = ScriptableObject.CreateInstance<Tile>();
                    tile.color = Color.white;
                    tile.sprite = tileTextures.baseTiles[_cells[x, y].Type];
                    tile.name = _cells[x, y].Type.ToString();
                    Vector3Int tilePosition = new Vector3Int(x, y, 0);
                    baseTilemap.SetTile(tilePosition, tile);
                }
            }
        }
        private void RenderOverlayTilemap()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = _height - 1; y >= 0; y--)
                {
                    Tile tile = ScriptableObject.CreateInstance<Tile>();
                    tile.color = Color.white;
                    tile.sprite = tileTextures.elementTiles[_cells[x, y].ElementType];
                    tile.name = _cells[x, y].ElementType.ToString();
                    Vector3Int tilePosition = new Vector3Int(x, y, 0);
                    overlayTilemap.SetTile(tilePosition, tile);
                }
            }
        }
        
        private void CreateEntities()
        {
            CreateWorker();
            CreateElements();
        }

        private void CreateWorker()
        {
            (int x, int y) workerStartTile = level1.StartTile;
            
            //Spawn the worker at the start tile position
            entitySpawner.Spawn(entities.workerEntity, workerStartTile.x, workerStartTile.y);
        }
        private void CreateElements()
        {
            for (var x = 0; x < _cells.GetLength(0); x++)
                for (var y = 0; y < _cells.GetLength(1); y++)
                {
                    var cellData = _cells[x, y];
                    if (cellData.ElementType is TileElementType.Empty or TileElementType.Rock
                                             or TileElementType.Road or TileElementType.Base)
                        continue;
                    // Spawn interactable entities based on the cell data
                    entitySpawner.Spawn(entities.elementEntities[cellData.ElementType], x, y);
                }
        }

        public void CreateRoad(float x, float y)
        {
            TileElementUpdate(x, y, TileElementType.Road);
        }
        
        public void RemoveRoad(float x, float y)
        {
            TileElementUpdate(x, y, TileElementType.Empty);
        }
        
        public void TileElementUpdate(float x, float y, TileElementType elementType)
        {
            Vector2Int gridPosition = GridUtilities.WorldPositionToGridPosition(new Vector3(x, y, 0));
            CellData cell = _cells[gridPosition.x, gridPosition.y];
            int gridX = gridPosition.x;
            int gridY = gridPosition.y;
            Vector3Int tilePosition = new Vector3Int(gridX, gridY, 0);
            cell.ElementType = elementType;
            var tile = ScriptableObject.CreateInstance<Tile>();
            tile.color = Color.white;
            tile.sprite = tileTextures.elementTiles[cell.ElementType];
            tile.name = cell.ElementType.ToString();
            overlayTilemap.SetTile(tilePosition, tile);
        } 
    }
}