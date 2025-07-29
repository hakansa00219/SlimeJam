using System.Collections.Generic;
using Data;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "Costs", menuName = "Scriptable/Costs")]
    public class Costs : SerializedScriptableObject
    {
        [OdinSerialize]
        public Dictionary<string, Cost> costs = new Dictionary<string, Cost>();

        public Cost Clone(string key)
        {
            return new Cost()
            {
                berry = costs[key].berry,
                wood = costs[key].wood,
                slime = costs[key].slime,
                metal = costs[key].metal
            };
        }
    }
}