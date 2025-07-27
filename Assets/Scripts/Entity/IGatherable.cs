using System.Collections.Generic;
using Engine;
using Map.Tiles;

namespace Entity
{
    public interface IGatherable
    {
        public bool IsGathered { get; set; }
        public bool IsPickedUp { get; set; }
        public TileElementType Type { get; }
        public void Initialize(EntitySpawner spawner, int x, int y);
        public TickActionBehaviour GatheringBehaviour();
        
        public Queue<IMaterial> SpawnedMaterials { get; set; }
    }
}