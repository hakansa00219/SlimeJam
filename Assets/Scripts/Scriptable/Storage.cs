using System;
using System.Collections.Generic;
using Data;
using Entity;
using Entity.Entities;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Structure;
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

        public bool CanAfford(Cost materials)
        {
            return ItemElements["Wood"].amount >= materials.wood &&
                   ItemElements["Berry"].amount >= materials.berry &&
                   ItemElements["Metal"].amount >= materials.metal &&
                   ItemElements["Slime"].amount >= materials.slime;
        }

        public void Spend(Cost materials)
        {
            foreach (var (_, value) in EntityContainer.Depositables)
            {
                if (value is not Warehouse warehouse) continue;
                
                IStorage.StorageInfo currentInfo = warehouse.CurrentInfo;
                
                // 10                         4
                // 3                          5
                // 6                          6
                if (materials.wood > 0 && currentInfo.Wood > 0)
                {
                    int woodAmount =  Math.Min(currentInfo.Wood, materials.wood);
                    materials.wood -= woodAmount;
                    currentInfo.Wood -= woodAmount;
                }
                if (materials.berry > 0 && currentInfo.Berry > 0)
                {
                    int berryAmount = Math.Min(currentInfo.Berry, materials.berry);
                    materials.berry -= berryAmount;
                    currentInfo.Berry -= berryAmount;
                }
                if (materials.metal > 0 && currentInfo.Metal > 0)
                {
                    int metalAmount = Math.Min(currentInfo.Metal, materials.metal);
                    materials.metal -= metalAmount;
                    currentInfo.Metal -= metalAmount;
                }
                if (materials.slime > 0 && currentInfo.Slime > 0)
                {
                    int slimeAmount = Math.Min(currentInfo.Slime, materials.slime);
                    materials.slime -= slimeAmount;
                    currentInfo.Slime -= slimeAmount;
                }
                
                warehouse.CurrentInfo = currentInfo;
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