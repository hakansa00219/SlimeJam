using System.Collections.Generic;
using Engine;
using Entity;
using Map.Tiles;
using Scriptable;
using UnityEngine;

namespace Worker.Actions
{
    public class SlimeGathering : TickActionBehaviour
    {
        protected override int TickDelay { get; set; } = 10;

        private EntitySpawner _spawner;
        private Entities _entities;
        private int _slimeTilePositionX;
        private int _slimeTilePositionY;
        private Queue<IMaterial> _spawnedMaterials;
        
        public void Initialize(EntitySpawner spawner, Entities entities, Queue<IMaterial> spawnedMaterials, int slimeTilePositionX, int slimeTilePositionY)
        {
            _spawner = spawner;
            _entities = entities;
            _spawnedMaterials = spawnedMaterials;
            _slimeTilePositionX = slimeTilePositionX;
            _slimeTilePositionY = slimeTilePositionY;
        }
        protected override void OnTick()
        {
            if (_spawner == null || _entities == null)
            {
                Debug.LogError("SlimeGathering not initialized. Call Initialize() before using.");
                return;
            }

            (int x,int y)[] possibleSpawnPositions = new (int,int)[]
            {
                (_slimeTilePositionX + 1, _slimeTilePositionY),
                (_slimeTilePositionX, _slimeTilePositionY - 1),
                (_slimeTilePositionX - 1, _slimeTilePositionY),
                (_slimeTilePositionX, _slimeTilePositionY + 1)
            };
            
            var winner = possibleSpawnPositions[Random.Range(0, possibleSpawnPositions.Length)];
            Debug.Log("Slime Gathered! " + System.DateTime.Now.ToString("HH:mm:ss.fff"));
            Transform spawnedObj = _spawner.Spawn(_entities.materialEntities[TileElementType.Slime], winner.x, winner.y);
            
            if (spawnedObj != null && spawnedObj.TryGetComponent<IMaterial>(out var spawnedMaterial))
            {
                _spawnedMaterials.Enqueue(spawnedMaterial);
            }
        }
    }
}