using System.Collections.Generic;
using Entity.Entities;
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
                EntityContainer.gatherables.TryAdd(gridPosition, gatherable);
                gatherable.Initialize(this, x, y);
            }


            return spawnedObj;
        }
    }
}