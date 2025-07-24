using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Engine;
using Entity.Entities.Worker.Actions;
using Map.Tiles;
using Structure;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Entity.Entities.Worker
{
    public class Worker : MonoBehaviour, IEntity, IStorage
    {
        public int GridPositionX { get; set; }
        public int GridPositionY { get; set; }
        public Tilemap OverlayTilemap { get; private set; }

        public IStorage.StorageInfo CurrentInfo { get; set; } = new IStorage.StorageInfo(0, 0, 0, 0);
        public IStorage.StorageCapacity Capacity { get; set; } = new IStorage.StorageCapacity(4, 2, 2, 2, 2);
        
        public void Initialize(Tilemap overlayTilemap, int x, int y)
        {
            OverlayTilemap = overlayTilemap;
            GridPositionX = x;
            GridPositionY = y;
        }

        public async UniTaskVoid OnMovementActionDone(WorkerMovement movement)
        {
            movement.isActive = false;
            
            await UniTask.SwitchToMainThread();
            await TickSystem.WaitForNextTickAsync();
            
            // Action logic is done. Check other tiles except roads for resources or tasks.
            Queue<IGatherable> gatherActionQueue = new Queue<IGatherable>();
            Queue<IMaterial> pickActionQueue = new Queue<IMaterial>();
            
            Vector3Int rightTilePosition = new Vector3Int(GridPositionX + 1, GridPositionY, 0);
            Vector3Int downTilePosition = new Vector3Int(GridPositionX, GridPositionY - 1, 0);
            Vector3Int leftTilePosition = new Vector3Int(GridPositionX - 1, GridPositionY, 0);
            Vector3Int upTilePosition = new Vector3Int(GridPositionX, GridPositionY + 1, 0);

            //Gather stuff first
            CheckGatherables(ref gatherActionQueue, rightTilePosition);
            CheckGatherables(ref gatherActionQueue, downTilePosition);
            CheckGatherables(ref gatherActionQueue, leftTilePosition);
            CheckGatherables(ref gatherActionQueue, upTilePosition);
            if (gatherActionQueue.Count > 0)
                await GatherActions(gatherActionQueue);
            
            //Then pick up materials
            CheckPickables(ref pickActionQueue, rightTilePosition);
            CheckPickables(ref pickActionQueue, downTilePosition);
            CheckPickables(ref pickActionQueue, leftTilePosition);
            CheckPickables(ref pickActionQueue, upTilePosition);
            if (pickActionQueue.Count > 0)
                await PickingActions(pickActionQueue);
            
            
            movement.isActive = true;
        }

        private async UniTask GatherActions(Queue<IGatherable> gatherActionQueue)
        {
            await UniTask.SwitchToMainThread();
            while (gatherActionQueue.Count > 0)
            {
                var action = gatherActionQueue.Dequeue();
                TickActionBehaviour actionBehaviour = action.GatheringBehaviour();
                actionBehaviour.isActive = true;
                await UniTask.WaitUntil(() => actionBehaviour.isActionDone);
                actionBehaviour.isActive = false;
                actionBehaviour.isActionDone = false;
                action.IsGathered = true;
                await UniTask.Yield();
            }
        }
        
        private async UniTask PickingActions(Queue<IMaterial> pickActionQueue)
        {
            await UniTask.SwitchToMainThread();
            while (pickActionQueue.Count > 0)
            {
                var action = pickActionQueue.Dequeue();
                PickingUp actionBehaviour = (PickingUp)action.PickingUpBehaviour();
                actionBehaviour.isActive = true;
                await UniTask.WaitUntil(() => actionBehaviour.isActionDone);
                actionBehaviour.isActive = false;
                actionBehaviour.isActionDone = false;
                await UniTask.Yield();
            }
        }

        private void CheckGatherables(ref Queue<IGatherable> gatherActionQueue, Vector3Int tilePosition)
        {
            Vector2Int gridPosition = new Vector2Int(tilePosition.x, tilePosition.y);
            if (!EntityContainer.Gatherables.TryGetValue(gridPosition, out IGatherable gatherable)) return;
            
            if (!gatherable.IsGathered)
                gatherActionQueue.Enqueue(gatherable);
            
        }

        private void CheckPickables(ref Queue<IMaterial> pickActionQueue, Vector3Int tilePosition)
        {
            Vector2Int gridPosition = new Vector2Int(tilePosition.x, tilePosition.y);
            if (!EntityContainer.Gatherables.TryGetValue(gridPosition, out IGatherable gatherable)) return;
            
            if (gatherable.IsPickedUp) return;
            if (gatherable.SpawnedMaterials.Count <= 0) return;
            if (CurrentInfo.Total >= Capacity.Total) return;

            var type = gatherable.Type;
            var material = gatherable.SpawnedMaterials.First();
            
            if (material.PickingUpBehaviour() is PickingUp pickingUp)
                pickingUp.Initialize(gatherable, this);

            bool CanPick(InteractableTileType t, int current, int capacity) => type == t && current < capacity;
            
            if(CanPick(InteractableTileType.Berry, CurrentInfo.Berry, Capacity.Berry) ||
               CanPick(InteractableTileType.Metal, CurrentInfo.Metal, Capacity.Metal) ||
               CanPick(InteractableTileType.Slime, CurrentInfo.Slime, Capacity.Slime) ||
               CanPick(InteractableTileType.Wood, CurrentInfo.Wood, Capacity.Wood))
            {
                pickActionQueue.Enqueue(material);
            }
        }
        
        private void CheckStructures(ref Queue<IDepositable> depositActionQueue, Vector3Int tilePosition)
        {
            Vector2Int gridPosition = new Vector2Int(tilePosition.x, tilePosition.y);
            if (!EntityContainer.Structures.TryGetValue(gridPosition, out IDepositable depositable)) return;
            
            if (depositable.IsDeposited) return;
            if (CurrentInfo.Total <= 0) return;
            
            depositable.Initialize(this);
            depositActionQueue.Enqueue(depositable);
        }

        public void LoopReset()
        {
            foreach (var (_, value) in EntityContainer.Gatherables)
            {
                value.IsGathered = false;
                value.IsPickedUp = false;
            }

            foreach (var (_, value) in EntityContainer.Structures)
            {
                value.IsDeposited = false;
            }
        }
        
    }
}