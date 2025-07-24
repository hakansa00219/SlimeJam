using System.Collections.Generic;
using UnityEngine;

namespace Entity.Entities
{
    public static class EntityContainer
    {
        public static readonly Dictionary<Vector2Int, IGatherable> Gatherables = new Dictionary<Vector2Int, IGatherable>(); 
        public static Dictionary<Vector2Int, List<IMaterial>> SpawnedMaterials = new Dictionary<Vector2Int, List<IMaterial>>();
    }
}