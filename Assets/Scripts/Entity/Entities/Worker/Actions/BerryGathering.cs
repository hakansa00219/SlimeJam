using System.Collections.Generic;
using Engine;
using Map.Tiles;
using UnityEngine;

namespace Entity.Entities.Worker.Actions
{
    public class BerryGathering : TickActionBehaviour
    {
        protected override int TickDelay { get; set; } = 10;

        private EntitySpawner _spawner;
        private Scriptable.Entities _entities;
        private int _berryTilePositionX;
        private int _berryTilePositionY;
        private Queue<IMaterial> _spawnedMaterials;
        
        public void Initialize(EntitySpawner spawner, Scriptable.Entities entities, Queue<IMaterial> spawnedMaterials, int berryTilePositionX, int berryTilePositionY)
        {
            _spawner = spawner;
            _entities = entities;
            _spawnedMaterials = spawnedMaterials;
            _berryTilePositionX = berryTilePositionX;
            _berryTilePositionY = berryTilePositionY;
        }
        protected override void OnTick()
        {
            if (_spawner == null || _entities == null)
            {
                Debug.LogError("BerryGathering not initialized. Call Initialize() before using.");
                return;
            }

            (int x,int y)[] possibleSpawnPositions = new (int,int)[]
            {
                (_berryTilePositionX + 1, _berryTilePositionY),
                (_berryTilePositionX, _berryTilePositionY - 1),
                (_berryTilePositionX - 1, _berryTilePositionY),
                (_berryTilePositionX, _berryTilePositionY + 1)
            };
            
            var winner = possibleSpawnPositions[Random.Range(0, possibleSpawnPositions.Length)];
            Debug.Log("Berry Gathered! " + System.DateTime.Now.ToString("HH:mm:ss.fff"));
            Transform spawnedObj = _spawner.Spawn(_entities.materialEntities[TileElementType.Berry], winner.x, winner.y);

            if (spawnedObj != null && spawnedObj.TryGetComponent<IMaterial>(out var spawnedMaterial))
            {
                _spawnedMaterials.Enqueue(spawnedMaterial);
            }
            
        }
    }
}