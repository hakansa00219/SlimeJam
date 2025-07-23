using System.Collections.Generic;
using Engine;
using Entity.Entities.Worker.Actions;
using UnityEngine;
using UnityEngine.UI;
using Worker.Actions;

namespace Entity.Entities.Tree
{
    public class Tree : MonoBehaviour, IGatherable
    {
        private EntitySpawner _entitySpawner;
        private WoodGathering _woodGathering;
        private Scriptable.Entities _entities;
        private int _gridPositionX;
        private int _gridPositionY;
        
        public TickActionBehaviour GatheringBehaviour() => _woodGathering;

        public bool isGathered { get; set; } = false;
        public Queue<IMaterial> SpawnedMaterials { get; set; } = new Queue<IMaterial>();

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
            
            _woodGathering = GetComponent<WoodGathering>();

            if (_woodGathering != null)
            {
                _woodGathering.Initialize(_entitySpawner, _entities, SpawnedMaterials, _gridPositionX, _gridPositionY);
            }
        }
    }
}