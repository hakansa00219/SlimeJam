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
        protected override int TickDelay => 10;

        private EntitySpawner _spawner;
        private Entities _entities;
        private int _slimeTilePositionX;
        private int _slimeTilePositionY;

        public void Initialize(EntitySpawner spawner, Entities entities, int slimeTilePositionX, int slimeTilePositionY)
        {
            _spawner = spawner;
            _entities = entities;
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
            
            var winner = possibleSpawnPositions[Random.Range(0, possibleSpawnPositions.Length - 1)];
            
            _spawner.Spawn(_entities.materialsEntities[InteractableTileType.Slime], winner.x, winner.y);
        }
    }
}