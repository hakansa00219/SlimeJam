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
        public void Initialize(IGatherable gatherable, ref IStorage storage)
        {
            _gatherable = gatherable;
            _storage = storage;
        }
        protected override void OnTick()
        {
            var material = _gatherable.SpawnedMaterials.Dequeue();
            material.Pickup();

            switch (_gatherable.Type)
            {
                case InteractableTileType.Wood:
                    _storage.CurrentInfo.Wood += 1;
                    break;
                case InteractableTileType.Metal:
                    break;
                case InteractableTileType.Berry:
                    break;
                case InteractableTileType.Slime:
                    break;
                case InteractableTileType.Flag:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            
        }
    }
}