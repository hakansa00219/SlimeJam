using System.Collections.Generic;
using Engine;
using Entity.Entities.Worker.Actions;
using Map.Tiles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entity.Entities.Berry
{
    public class Berry : MonoBehaviour, IGatherable
    {
        private EntitySpawner _entitySpawner;
        private BerryGathering _berryGathering;
        private Scriptable.Entities _entities;
        private int _gridPositionX;
        private int _gridPositionY;
        
        public TickActionBehaviour GatheringBehaviour() => _berryGathering;
        public Queue<IMaterial> SpawnedMaterials { get; set; } = new Queue<IMaterial>();

        public bool IsGathered { get; set; } = false;
        public bool IsPickedUp { get; set; } = false;
        public InteractableTileType Type => InteractableTileType.Berry;

        public void Initialize(EntitySpawner spawner, int x, int y)
        {
            _entitySpawner = spawner;
            _gridPositionX = x;
            _gridPositionY = y;
            
            
            _entities = Resources.Load("Data/Entities/Entities", typeof(Scriptable.Entities)) as Scriptable.Entities;
            
            if (_entities == null)
            {
                Debug.LogError("Entities scriptable object not found. Ensure it is correctly set up in Resources/Data/Entities/Entities.asset");
            }
            
            _berryGathering = GetComponent<BerryGathering>();

            if (_berryGathering != null)
            {
                _berryGathering.Initialize(_entitySpawner, _entities, SpawnedMaterials, _gridPositionX, _gridPositionY);
            }
        }
    }
}