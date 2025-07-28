using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "Storage", menuName = "Scriptable/Storage")]
    public class Storage : SerializedScriptableObject
    {
        [OdinSerialize]
        public Dictionary<string, ItemElement> ItemElements = new Dictionary<string, ItemElement>();

        public void ClearAmounts()
        {
            foreach (var (_, value) in ItemElements)
            {
                value.amount = 0;
            }
        }
        
        [Serializable]
        public class ItemElement
        {
            public Sprite icon;
            public int amount;
        }
    }
}