using System;
using Entity;
using Scriptable;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Grid
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GridMap level1;
        [SerializeField] private Tilemap baseTilemap;
        [SerializeField] private Tilemap overlayTilemap;
        [SerializeField] private TileTextures tileTextures;
        [SerializeField] private Entities entities;
        
        private CellData[,] _cells;
        private int _width;
        private int _height;
        
        private EntitySpawner _entitySpawner;
        
        private void Awake()
        {
            _entitySpawner = new EntitySpawner();
            
            _cells = level1.Cells;
            _width = _cells.GetLength(0);
            _height = _cells.GetLength(1);
            
            CreateTilemaps();
            RenderTilemaps();
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
                for (int y = 0; y < _height; y++)
                {
                    Tile tile = ScriptableObject.CreateInstance<Tile>();
                    tile.color = Color.white;
                    tile.sprite = tileTextures.baseTiles[_cells[x, y].Type];
                    Vector3Int tilePosition = new Vector3Int(_cells[x, y].Position.x, _cells[x, y].Position.y, 0);
                    baseTilemap.SetTile(tilePosition, tile);
                }
            }
        }
        private void RenderOverlayTilemap()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    Tile tile = ScriptableObject.CreateInstance<Tile>();
                    tile.color = Color.white;
                    tile.sprite = tileTextures.overlayTiles[_cells[x, y].ElementType];
                    Vector3Int tilePosition = new Vector3Int(_cells[x, y].Position.x, _cells[x, y].Position.y, 0);
                    overlayTilemap.SetTile(tilePosition, tile);
                }
            }throw new NotImplementedException();
        }

        private void Start()
        {
            (int x, int y) workerStartTile = level1.StartTile;
            
            //Spawn the worker at the start tile position
            _entitySpawner.Spawn(entities.workerEntity, workerStartTile.x, workerStartTile.y);
        }
    }
}