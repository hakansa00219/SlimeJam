using System;
using System.Collections.Generic;
using Engine;
using Map.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;
using Worker.Actions;

namespace Entity.Entities.Slime
{
    [RequireComponent(typeof(SlimeGathering))]
    public class Slime : MonoBehaviour, IGatherable
    {
        private EntitySpawner _entitySpawner;
        private SlimeGathering _slimeGathering;
        private Scriptable.Entities _entities;
        private int _gridPositionX;
        private int _gridPositionY;
        
        public TickActionBehaviour GatheringBehaviour() => _slimeGathering;

        public bool IsGathered { get; set; } = false;
        public bool IsPickedUp { get; set; } = false;
        public Queue<IMaterial> SpawnedMaterials { get; set; } = new Queue<IMaterial>();

        public TileElementType Type { get; } = TileElementType.Slime;

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
            
            _slimeGathering = GetComponent<SlimeGathering>();

            if (_slimeGathering != null)
            {
                _slimeGathering.Initialize(_entitySpawner, _entities, SpawnedMaterials, _gridPositionX, _gridPositionY);
            }
        }

       
    }
}