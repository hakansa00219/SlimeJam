using System.Collections.Generic;
using Engine;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Entity.Entities.Worker
{
    public class Worker : MonoBehaviour, IEntity
    {
        public int GridPositionX { get; set; }
        public int GridPositionY { get; set; }
        public Tilemap OverlayTilemap { get; private set; }
 

        public void Initialize(Tilemap overlayTilemap, int x, int y)
        {
            OverlayTilemap = overlayTilemap;
            GridPositionX = x;
            GridPositionY = y;
        }

        public void OnActionDone(TickActionBehaviour action)
        {
            action.isActive = false;
            // Action logic is done. Check other tiles except roads for resources or tasks.
            
            Queue<TickActionBehaviour> actionQueue = new Queue<TickActionBehaviour>();
            
            Vector3Int rightTilePosition = new Vector3Int(GridPositionX + 1, GridPositionY, 0);
            Vector3Int downTilePosition = new Vector3Int(GridPositionX, GridPositionY - 1, 0);
            Vector3Int leftTilePosition = new Vector3Int(GridPositionX - 1, GridPositionY, 0);
            Vector3Int upTilePosition = new Vector3Int(GridPositionX, GridPositionY + 1, 0);

            CheckTile(ref actionQueue, rightTilePosition);
            CheckTile(ref actionQueue, downTilePosition);
            CheckTile(ref actionQueue, leftTilePosition);
            CheckTile(ref actionQueue, upTilePosition);
            // Depending to that continue to next movement or stop until next task is done.
        }

        private void CheckTile(ref Queue<TickActionBehaviour> actionQueue, Vector3Int rightTilePosition)
        {
            
            string tileName = InteractableTilemap.GetTile(rightTilePosition).name;
            switch (tileName)
            {
                case "Slime":
                    // Do slime action 
                    actionQueue.Enqueue(spaw);
                    break;
                case "Wood":
                    // Do wood action
                    break;
                case "Metal":
                    // Do metal action
                    break;
                case "Berry":
                    // Do berry action
                    break;
                case "Flag":
                    // Do flag action
                    break;
            }
        }
    }
}