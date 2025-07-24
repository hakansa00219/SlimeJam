using System.Collections.Generic;
using Structure;
using UnityEngine;

namespace Entity.Entities
{
    public static class EntityContainer
    {
        public static readonly Dictionary<Vector2Int, IGatherable> Gatherables = new Dictionary<Vector2Int, IGatherable>(); 
        public static readonly Dictionary<Vector2Int, IDepositable> Structures = new Dictionary<Vector2Int, IDepositable>();
    }
}