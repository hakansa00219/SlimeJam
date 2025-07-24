using System;
using Engine;
using Map.Tiles;

namespace Entity.Entities.Worker.Actions
{
    public class PickingUp : TickActionBehaviour
    {
        protected override int TickDelay => 3;
        
        private IGatherable _gatherable;
        private IStorage _storage;
        public void Initialize(IGatherable gatherable, IStorage storage)
        {
            _gatherable = gatherable;
            _storage = storage;
        }
        protected override void OnTick()
        {
            var material = _gatherable.SpawnedMaterials.Dequeue();
            material.Pickup();
            
            IStorage.StorageInfo info = _storage.CurrentInfo;

            switch (_gatherable.Type)
            {
                case InteractableTileType.Wood:
                    info.Wood += 1;
                    break;
                case InteractableTileType.Metal:
                    info.Metal += 1;
                    break;
                case InteractableTileType.Berry:
                    info.Berry += 1;
                    break;
                case InteractableTileType.Slime:
                    info.Slime += 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            _storage.CurrentInfo = info;
            _gatherable.IsPickedUp = true;

        }
    }
}