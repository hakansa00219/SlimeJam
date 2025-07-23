using Engine;
using Map.Tiles;
using UnityEngine;

namespace Entity.Entities.Worker.Actions
{
    public class MetalGathering : TickActionBehaviour
    {
        protected override int TickDelay => 10;
        
        private EntitySpawner _spawner;
        private Scriptable.Entities _entities;
        private int _metalTilePositionX;
        private int _metalTilePositionY;
        
        public void Initialize(EntitySpawner spawner, Scriptable.Entities entities, int metalTilePositionX, int metalTilePositionY)
        {
            _spawner = spawner;
            _entities = entities;
            _metalTilePositionX = metalTilePositionX;
            _metalTilePositionY = metalTilePositionY;
        }
        protected override void OnTick()
        {
            if (_spawner == null || _entities == null)
            {
                Debug.LogError("MetalGathering not initialized. Call Initialize() before using.");
                return;
            }

            (int x,int y)[] possibleSpawnPositions = new (int,int)[]
            {
                (_metalTilePositionX + 1, _metalTilePositionY),
                (_metalTilePositionX, _metalTilePositionY - 1),
                (_metalTilePositionX - 1, _metalTilePositionY),
                (_metalTilePositionX, _metalTilePositionY + 1)
            };
            
            var winner = possibleSpawnPositions[Random.Range(0, possibleSpawnPositions.Length)];
            Debug.Log("Metal Gathered! " + System.DateTime.Now.ToString("HH:mm:ss.fff"));
            _spawner.Spawn(_entities.materialsEntities[InteractableTileType.Metal], winner.x, winner.y);
        }
    }
}