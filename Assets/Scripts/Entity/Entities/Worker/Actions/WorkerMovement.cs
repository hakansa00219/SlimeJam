using Engine;
using Map.Tiles;
using UnityEngine;
using Utility;

namespace Entity.Entities.Worker.Actions
{
    [RequireComponent(typeof(Worker))]
    public class WorkerMovement : TickActionBehaviour
    {
        protected override int TickDelay { get; set; } = 3;
        
        private Worker _worker;
        private Vector2Int _startTilePosition;
        private Vector3Int _oneTickBeforePosition;
        private Vector2Int _direction = Vector2Int.zero;

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
                _worker.LoopReset();
                if(_direction == Vector2Int.zero)
                    _direction = Vector2Int.left;

            }
            Vector3Int nextTilePosition = new Vector3Int(-1,-1,-1);
            Vector2Int[] checkTiles = new Vector2Int[4];
            checkTiles = CheckTiles(_direction);

            foreach (Vector2Int checkTile in checkTiles)
            {
                Vector3Int tile = new Vector3Int(checkTile.x, checkTile.y, 0);
                var tileElement = _worker.OverlayTilemap.GetTile(tile);
                if (tileElement != null && tileElement.name == TileElementType.Road.ToString() /*&&
                    !_visitedTiles.Contains(tile)*/)
                {
                    nextTilePosition = tile;
                    _direction = new Vector2Int(nextTilePosition.x - _worker.GridPositionX, nextTilePosition.y - _worker.GridPositionY);
                    break;
                }
            }
            
            _worker.transform.position = GridUtilities.GridPositionToWorldPosition(new Vector2Int(nextTilePosition.x, nextTilePosition.y));
            _worker.GridPositionX = nextTilePosition.x;
            _worker.GridPositionY = nextTilePosition.y;
            
            _worker.OnMovementActionDone(this);
        }

        private Vector2Int[] CheckTiles(Vector2Int direction)
        {
            Vector2Int[] checkTiles = new Vector2Int[4];
            if (direction == Vector2Int.right) // DOWN, RIGHT, UP, LEFT
            {
                checkTiles[0] = new Vector2Int(_worker.GridPositionX, _worker.GridPositionY - 1); 
                checkTiles[1] = new Vector2Int(_worker.GridPositionX + 1, _worker.GridPositionY); 
                checkTiles[2] = new Vector2Int(_worker.GridPositionX, _worker.GridPositionY + 1); 
                checkTiles[3] = new Vector2Int(_worker.GridPositionX - 1, _worker.GridPositionY); 
            }
            else if (direction == Vector2Int.down) // LEFT, DOWN, RIGHT, UP
            {
                checkTiles[0] = new Vector2Int(_worker.GridPositionX - 1, _worker.GridPositionY); 
                checkTiles[1] = new Vector2Int(_worker.GridPositionX, _worker.GridPositionY - 1); 
                checkTiles[2] = new Vector2Int(_worker.GridPositionX + 1, _worker.GridPositionY); 
                checkTiles[3] = new Vector2Int(_worker.GridPositionX, _worker.GridPositionY + 1); 
            }
            else if (direction == Vector2Int.left) // UP, LEFT, DOWN, RIGHT
            {
                checkTiles[0] = new Vector2Int(_worker.GridPositionX, _worker.GridPositionY + 1); 
                checkTiles[1] = new Vector2Int(_worker.GridPositionX - 1, _worker.GridPositionY); 
                checkTiles[2] = new Vector2Int(_worker.GridPositionX, _worker.GridPositionY - 1); 
                checkTiles[3] = new Vector2Int(_worker.GridPositionX + 1, _worker.GridPositionY); 
            }
            else if (direction == Vector2Int.up) // RIGHT, UP, LEFT, DOWN
            {
                checkTiles[0] = new Vector2Int(_worker.GridPositionX + 1, _worker.GridPositionY);
                checkTiles[1] = new Vector2Int(_worker.GridPositionX, _worker.GridPositionY + 1);
                checkTiles[2] = new Vector2Int(_worker.GridPositionX - 1, _worker.GridPositionY);
                checkTiles[3] = new Vector2Int(_worker.GridPositionX, _worker.GridPositionY - 1);
            }
            else
            {
                checkTiles = null;
            }

            return checkTiles;
        }
        
    }
}