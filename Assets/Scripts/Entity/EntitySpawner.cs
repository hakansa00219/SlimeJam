using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Entity
{
    public class EntitySpawner : Spawner, ISpawner
    {
        public Dictionary<Vector2Int, IGatherable> gatherables = new Dictionary<Vector2Int, IGatherable>(); 
        
        public void Spawn(Transform prefab, int x, int y)
        {
            Vector2Int gridPosition = new Vector2Int(x, y);
            Vector3 worldPosition = GridUtilities.GridPositionToWorldPosition(gridPosition);
            
            Transform obj = Instantiate(prefab, worldPosition, Quaternion.identity, container);
            
            IEntity entity = obj.GetComponent<IEntity>();
            if(entity != null)
                entity.Initialize(overlayTilemap, x, y);
            
            IGatherable gatherable = obj.GetComponent<IGatherable>();
            if (gatherable != null)
            {
                gatherables.TryAdd(gridPosition, gatherable);
                gatherable.Initialize(this, x, y);
            }
            
        }
    }

}