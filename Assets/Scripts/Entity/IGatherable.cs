using System.Collections.Generic;
using Engine;
using Map.Tiles;

namespace Entity
{
    public interface IGatherable
    {
        public bool IsGathered { get; set; }
        public InteractableTileType Type { get; }
        public void Initialize(EntitySpawner spawner, int x, int y);
        public TickActionBehaviour GatheringBehaviour();
        
        public Queue<IMaterial> SpawnedMaterials { get; set; }
    }
}