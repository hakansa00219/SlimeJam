using System.Collections.Generic;
using Entity.Entities;
using Entity.Entities.Flag;
using Structure;
using UnityEngine;
using Utility;

namespace Entity
{
    public class EntitySpawner : Spawner, ISpawner
    {
        public Transform Spawn(Transform prefab, int x, int y)
        {
            Vector2Int gridPosition = new Vector2Int(x, y);
            Vector3 worldPosition = GridUtilities.GridPositionToWorldPosition(gridPosition);
            
            Transform spawnedObj = Instantiate(prefab, worldPosition, Quaternion.identity, container);
            
            IEntity entity = spawnedObj.GetComponent<IEntity>();
            if(entity != null)
                entity.Initialize(overlayTilemap, x, y);
            
            IGatherable gatherable = spawnedObj.GetComponent<IGatherable>();
            if (gatherable != null)
            {
                EntityContainer.Gatherables.TryAdd(gridPosition, gatherable);
                gatherable.Initialize(this, x, y);
            }
            IConvertable convertable = spawnedObj.GetComponent<IConvertable>();
            if (convertable != null)
            {
                EntityContainer.Convertables.TryAdd(gridPosition, convertable);
            }
            IDepositable depositable = spawnedObj.GetComponent<IDepositable>();
            if (depositable != null)
                EntityContainer.Structures.TryAdd(gridPosition, depositable);


            return spawnedObj;
        }
        
        public Transform Spawn(Transform prefab, float x, float y)
        {
            Vector2Int gridPosition = GridUtilities.WorldPositionToGridPosition(new Vector3(x, y, 0));
            Vector3 worldPosition = GridUtilities.GridPositionToWorldPosition(gridPosition);
            
            Transform spawnedObj = Instantiate(prefab, worldPosition, Quaternion.identity, container);
            
            IEntity entity = spawnedObj.GetComponent<IEntity>();
            if(entity != null)
                entity.Initialize(overlayTilemap, gridPosition.x, gridPosition.y);
            
            IGatherable gatherable = spawnedObj.GetComponent<IGatherable>();
            if (gatherable != null)
            {
                EntityContainer.Gatherables.TryAdd(gridPosition, gatherable);
                gatherable.Initialize(this, gridPosition.x, gridPosition.y);
            }
            IDepositable depositable = spawnedObj.GetComponent<IDepositable>();
            if (depositable != null)
                EntityContainer.Structures.TryAdd(gridPosition, depositable);


            return spawnedObj;
        }
    }
}