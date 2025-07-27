using System.Collections.Generic;
using Engine;
using Map.Tiles;
using UnityEngine;

namespace Entity.Entities.Worker.Actions
{
    public class WoodGathering : TickActionBehaviour
    {
        protected override int TickDelay => 10;
        
        private EntitySpawner _spawner;
        private Scriptable.Entities _entities;
        private int _woodTilePositionX;
        private int _woodTilePositionY;
        private Queue<IMaterial> _spawnedMaterials;
        
        public void Initialize(EntitySpawner spawner, Scriptable.Entities entities, Queue<IMaterial> spawnedMaterials, int woodTilePositionX, int woodTilePositionY)
        {
            _spawner = spawner;
            _entities = entities;
            _spawnedMaterials = spawnedMaterials;
            _woodTilePositionX = woodTilePositionX;
            _woodTilePositionY = woodTilePositionY;
        }
        protected override void OnTick()
        {
            if (_spawner == null || _entities == null)
            {
                Debug.LogError("WoodGathering not initialized. Call Initialize() before using.");
                return;
            }

            (int x,int y)[] possibleSpawnPositions = new (int,int)[]
            {
                (_woodTilePositionX + 1, _woodTilePositionY),
                (_woodTilePositionX, _woodTilePositionY - 1),
                (_woodTilePositionX - 1, _woodTilePositionY),
                (_woodTilePositionX, _woodTilePositionY + 1)
            };
            
            var winner = possibleSpawnPositions[Random.Range(0, possibleSpawnPositions.Length)];
            Debug.Log("Wood Gathered! " + System.DateTime.Now.ToString("HH:mm:ss.fff"));
            Transform spawnedObj =  _spawner.Spawn(_entities.materialEntities[TileElementType.Wood], winner.x, winner.y);
            
            if (spawnedObj != null && spawnedObj.TryGetComponent<IMaterial>(out var spawnedMaterial))
            {
                _spawnedMaterials.Enqueue(spawnedMaterial);
            }
        }
    }
}