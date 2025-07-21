using System;
using System.Collections.Generic;
using Engine;
using Map.Tiles;
using UnityEngine;
using Utility;

namespace Worker
{
    [RequireComponent(typeof(Worker))]
    public class WorkerMovement : TickActionBehaviour
    {
        private Worker _worker;
        private HashSet<Vector3Int> _visitedTiles = new HashSet<Vector3Int>();
        private Vector2Int _startTilePosition;

        private void Awake()
        {
            _worker = GetComponent<Worker>();
            _startTilePosition = GridUtilities.WorldPositionToGridPosition(transform.position);
        }

        protected override void OnTick()
        {
            Debug.Log("Move");
            
            // If you are in the start tile, reset the visited tiles
            if (_worker.GridPositionX == _startTilePosition.x && _worker.GridPositionY == _startTilePosition.y)
            {
                _visitedTiles.Clear();
            }
            
            //Check neighbour tiles and 
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
                Debug.LogError("No valid tile to move to.");
                return;
            }

            _worker.transform.position = GridUtilities.GridPositionToWorldPosition(new Vector2Int(nextTilePosition.x, nextTilePosition.y));
            _visitedTiles.Add(nextTilePosition);
            _worker.GridPositionX = nextTilePosition.x;
            _worker.GridPositionY = nextTilePosition.y;
        }
    }
}