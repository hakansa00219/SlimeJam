using System.Collections.Generic;
using Map.Tiles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "Entities", menuName = "Scriptable/Entities")]
    public class Entities : SerializedScriptableObject
    {
        public Dictionary<TileElementType, Transform> elementEntities = new Dictionary<TileElementType, Transform>();
        public Dictionary<TileElementType, Transform> materialEntities = new Dictionary<TileElementType, Transform>();
        public Transform workerEntity;
    }
}