using System.Collections.Generic;
using Map.Tiles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "Entities", menuName = "Scriptable/Entities")]
    public class Entities : SerializedScriptableObject
    {
        public Dictionary<InteractableTileType, Transform> interactableEntities = new Dictionary<InteractableTileType, Transform>();
        public Transform workerEntity;
    }
}