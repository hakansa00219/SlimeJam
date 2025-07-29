using System;
using Data;
using Entity.Entities;
using Scriptable;
using UI;
using UnityEngine;
using Utility;

namespace Structure
{
    public class UpgradeSystem : MonoBehaviour
    {
        [SerializeField] private Costs warehouseUpgrades;
        [SerializeField] private Storage storage;

        public Cost WarehouseStorageUpgrade { get; private set; }
        private void Awake()
        {
            WarehouseStorageUpgrade = warehouseUpgrades.Clone("Storage");
            BuyableActions.WarehouseActions.TryAdd("Storage", UpgradeWarehouseStorage);
        }

        private void UpgradeWarehouseStorage(float x, float y)
        {
            Vector2Int gridPosition = GridUtilities.WorldPositionToGridPosition(new Vector3(x, y));
            Warehouse warehouse = EntityContainer.Depositables[gridPosition] as Warehouse;
            
            if (warehouse == null)
            {
                Debug.LogError($"No warehouse found at position {gridPosition}");
                return;
            }
            
            Cost cost = WarehouseStorageUpgrade;
            Cost nextCost = new Cost(WarehouseStorageUpgrade);
            storage.Spend(cost);
            WarehouseStorageUpgrade = new Cost()
            {
                wood = nextCost.wood * 2,
                berry = nextCost.berry * 2,
                slime = nextCost.slime * 2,
                metal = nextCost.metal * 2
            };
            warehouse.UpgradeCapacity();
        }
    }
}