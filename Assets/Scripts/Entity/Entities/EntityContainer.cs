using System.Collections.Generic;
using UnityEngine;

namespace Entity.Entities
{
    public class EntityContainer
    {
        public static Dictionary<Vector2Int, IGatherable> gatherables = new Dictionary<Vector2Int, IGatherable>(); 
        public static Dictionary<Vector2Int, List<IMaterial>> spawnedMaterials = new Dictionary<Vector2Int, List<IMaterial>>();
    }
}