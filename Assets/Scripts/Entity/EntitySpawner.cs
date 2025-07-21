using UnityEngine;
using Utility;

namespace Entity
{
    public class EntitySpawner : Spawner, ISpawner
    {
        public void Spawn(Transform prefab, int x, int y)
        {
            Vector3 worldPosition = GridUtilities.GridPositionToWorldPosition(new Vector2Int(x, y));
            Transform obj = Instantiate(prefab, worldPosition, Quaternion.identity, container);
            IEntity entity = obj.GetComponent<IEntity>();
            if(entity != null)
                entity.Initialize(overlayTilemap, x, y);
        }
    }

}