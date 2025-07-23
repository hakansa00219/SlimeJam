using System.Collections.Generic;
using Engine;
using Map.Tiles;
using UnityEngine;
using Utility;

namespace Entity.Entities.Worker.Actions
{
    [RequireComponent(typeof(Worker))]
    public class WorkerMovement : TickActionBehaviour
    {
        protected override int TickDelay => 3;
        
        private Worker _worker;
        private readonly HashSet<Vector3Int> _visitedTiles = new HashSet<Vector3Int>();
        private Vector2Int _startTilePosition;
        private Vector3Int _lastTilePosition;

        private void Awake()
        {
            _worker = GetComponent<Worker>();
            _startTilePosition = GridUtilities.WorldPositionToGridPosition(transform.position);
            isActive = true;
        }
        
        protected override void OnTick()
        {
            // If you are in the start tile, reset the visited tiles
            if (_worker.GridPositionX == _startTilePosition.x && _worker.GridPositionY == _startTilePosition.y)
            {
                _visitedTiles.Clear();
                _worker.LoopReset();
            }
            
            //Check neighbour tiles and move to the first valid tile
            Vector3Int rightTilePosition = new Vector3Int(_worker.GridPositionX + 1, _worker.GridPositionY, 0);
            Vector3Int downTilePosition = new Vector3Int(_worker.GridPositionX, _worker.GridPositionY - 1, 0);
            Vector3Int leftTilePosition = new Vector3Int(_worker.GridPositionX - 1, _worker.GridPositionY, 0);
            Vector3Int upTilePosition = new Vector3Int(_worker.GridPositionX, _worker.GridPositionY + 1, 0);

            Vector3Int nextTilePosition;

            if (_worker.OverlayTilemap.GetTile(rightTilePosition).name == TileElementType.Road.ToString() &&
                !_visitedTiles.Contains(rightTilePosition))
            {
                nextTilePosition = rightTilePosition;
            }
            else if (_worker.OverlayTilemap.GetTile(downTilePosition).name == TileElementType.Road.ToString() &&
                     !_visitedTiles.Contains(downTilePosition))
            {
                nextTilePosition = downTilePosition;
            }
            else if (_worker.OverlayTilemap.GetTile(leftTilePosition).name == TileElementType.Road.ToString() &&
                     !_visitedTiles.Contains(leftTilePosition))
            {
                nextTilePosition = leftTilePosition;
            }
            else if (_worker.OverlayTilemap.GetTile(upTilePosition).name == TileElementType.Road.ToString() &&
                     !_visitedTiles.Contains(leftTilePosition))
            {
                nextTilePosition = upTilePosition;
            }
            else
            {
                Debug.LogWarning("No valid tile to move to. Backing up to last tile.");
                // Go back once
                _worker.transform.position = GridUtilities.GridPositionToWorldPosition(new Vector2Int(_lastTilePosition.x, _lastTilePosition.y));
                _visitedTiles.Add(_lastTilePosition);
                _worker.GridPositionX = _lastTilePosition.x;
                _worker.GridPositionY = _lastTilePosition.y;
                return;
            }

            _lastTilePosition = new Vector3Int(_worker.GridPositionX, _worker.GridPositionY, 0);
            _worker.transform.position = GridUtilities.GridPositionToWorldPosition(new Vector2Int(nextTilePosition.x, nextTilePosition.y));
            _visitedTiles.Add(nextTilePosition);
            _worker.GridPositionX = nextTilePosition.x;
            _worker.GridPositionY = nextTilePosition.y;
            
            _worker.OnMovementActionDone(this);
        }
    }
}