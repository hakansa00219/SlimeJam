using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Engine;
using Entity.Entities.Flag;
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
            Queue<IConvertable> convertableActionQueue = new Queue<IConvertable>();
            
            Vector3Int[] tilePositions = new Vector3Int[4]
            {
                new Vector3Int(GridPositionX + 1, GridPositionY, 0), // Right
                new Vector3Int(GridPositionX, GridPositionY - 1, 0), // Down
                new Vector3Int(GridPositionX - 1, GridPositionY, 0), // Left
                new Vector3Int(GridPositionX, GridPositionY + 1, 0)  // Up
            };

            //Gather stuff first
            CheckGatherables(ref gatherActionQueue, tilePositions);
            if (gatherActionQueue.Count > 0)
                await GatherActions(gatherActionQueue);
            
            //Then pick up materials
            CheckPickables(ref pickActionQueue, tilePositions);
            if (pickActionQueue.Count > 0)
                await PickingActions(pickActionQueue);
            
            //Then pick up materials
            CheckFlag(ref convertableActionQueue, tilePositions);
            if (convertableActionQueue.Count > 0)
                await ConvertingActions(convertableActionQueue);
            
            
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
        
        private async UniTask ConvertingActions(Queue<IConvertable> convertActionQueue)
        {
            await UniTask.SwitchToMainThread();
            while (convertActionQueue.Count > 0)
            {
                var action = convertActionQueue.Dequeue();
                TickActionBehaviour actionBehaviour = action.ConvertingBehaviour();
                actionBehaviour.isActive = true;
                await UniTask.WaitUntil(() => actionBehaviour.isActionDone);
                actionBehaviour.isActive = false;
                actionBehaviour.isActionDone = false;
                await UniTask.Yield();
            }
        }

        private void CheckGatherables(ref Queue<IGatherable> gatherActionQueue, Vector3Int[] tilePosition)
        {
            foreach (var t in tilePosition)
            {
                Vector2Int gridPosition = new Vector2Int(t.x, t.y);
                if (!EntityContainer.Gatherables.TryGetValue(gridPosition, out IGatherable gatherable)) continue;
            
                if (!gatherable.IsGathered)
                    gatherActionQueue.Enqueue(gatherable);
            }
        }

        private void CheckPickables(ref Queue<IMaterial> pickActionQueue, Vector3Int[] tilePosition)
        {
            foreach (var t1 in tilePosition)
            {
                Vector2Int gridPosition = new Vector2Int(t1.x, t1.y);
                if (!EntityContainer.Gatherables.TryGetValue(gridPosition, out IGatherable gatherable)) continue;
            
                if (gatherable.IsPickedUp) continue;
                if (gatherable.SpawnedMaterials.Count <= 0) continue;
                if (CurrentInfo.Total >= Capacity.Total) continue;

                var type = gatherable.Type;
                var material = gatherable.SpawnedMaterials.First();
            
                if (material.PickingUpBehaviour() is PickingUp pickingUp)
                    pickingUp.Initialize(gatherable, this);

                bool CanPick(TileElementType t, int current, int capacity) => type == t && current < capacity;
            
                if(CanPick(TileElementType.Berry, CurrentInfo.Berry, Capacity.Berry) ||
                   CanPick(TileElementType.Metal, CurrentInfo.Metal, Capacity.Metal) ||
                   CanPick(TileElementType.Slime, CurrentInfo.Slime, Capacity.Slime) ||
                   CanPick(TileElementType.Wood, CurrentInfo.Wood, Capacity.Wood))
                {
                    pickActionQueue.Enqueue(material);
                }
            }
        }
        
        private void CheckFlag(ref Queue<IConvertable> flagActionQueue, Vector3Int[] tilePosition)
        {
            foreach (var t in tilePosition)
            {
                Vector2Int gridPosition = new Vector2Int(t.x, t.y);
                if (!EntityContainer.Convertables.TryGetValue(gridPosition, out IConvertable convertable)) continue;
            
                if (!convertable.IsConverted)
                    flagActionQueue.Enqueue(convertable);
            }
        }
        
        private void CheckStructures(ref Queue<IDepositable> depositActionQueue, Vector3Int tilePosition)
        {
            Vector2Int gridPosition = new Vector2Int(tilePosition.x, tilePosition.y);
            if (!EntityContainer.Depositables.TryGetValue(gridPosition, out IDepositable depositable)) return;
            
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

            foreach (var (_, value) in EntityContainer.Depositables)
            {
                value.IsDeposited = false;
            }
        }
        
    }
}