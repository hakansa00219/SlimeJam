using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Engine;
using Entity.Entities.Worker.Actions;
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

        public void OnMovementActionDone(WorkerMovement movement)
        {
            movement.isActive = false;
            // Action logic is done. Check other tiles except roads for resources or tasks.
            Debug.Log("Waiting for next tick... " + System.DateTime.Now.ToString("HH:mm:ss.fff"));
            Queue<IGatherable> actionQueue = new Queue<IGatherable>();
            
            Vector3Int rightTilePosition = new Vector3Int(GridPositionX + 1, GridPositionY, 0);
            Vector3Int downTilePosition = new Vector3Int(GridPositionX, GridPositionY - 1, 0);
            Vector3Int leftTilePosition = new Vector3Int(GridPositionX - 1, GridPositionY, 0);
            Vector3Int upTilePosition = new Vector3Int(GridPositionX, GridPositionY + 1, 0);

            CheckTile(ref actionQueue, rightTilePosition);
            CheckTile(ref actionQueue, downTilePosition);
            CheckTile(ref actionQueue, leftTilePosition);
            CheckTile(ref actionQueue, upTilePosition);
            // Depending to that continue to next movement or stop until next task is done.

            if (actionQueue.Count > 0)
                QueueActions(actionQueue, movement);
            else
                movement.isActive = true;
        }

        private async UniTaskVoid QueueActions(Queue<IGatherable> actionQueue, WorkerMovement movement)
        {
            await TickSystem.WaitForNextTickAsync();
            Debug.Log("Tick occurred! " + System.DateTime.Now.ToString("HH:mm:ss.fff"));
            await UniTask.SwitchToMainThread();
            while (actionQueue.Count > 0)
            {
                var action = actionQueue.Dequeue();
                TickActionBehaviour actionBehaviour = action.GatheringBehaviour();
                actionBehaviour.isActive = true;

                await UniTask.WaitUntil(() => actionBehaviour.isActionDone);
                actionBehaviour.isActive = false;
                actionBehaviour.isActionDone = false;
                action.isGathered = true;
                await UniTask.Yield();
            }
            movement.isActive = true;
        }

        private void CheckTile(ref Queue<IGatherable> actionQueue, Vector3Int rightTilePosition)
        {
            Vector2Int gridPosition = new Vector2Int(rightTilePosition.x, rightTilePosition.y);
            if (!EntityContainer.gatherables.TryGetValue(gridPosition, out IGatherable gatherable)) return;
            
            if (!gatherable.isGathered)
                actionQueue.Enqueue(gatherable);
        }

        public void LoopReset()
        {
            foreach (var (_, value) in EntityContainer.gatherables)
            {
                value.isGathered = false;
            }
        }
    }
}