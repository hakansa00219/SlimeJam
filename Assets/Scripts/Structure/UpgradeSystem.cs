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
        [SerializeField] private Costs gymUpgrades;
        [SerializeField] private Storage storage;

        public Cost WarehouseStorageUpgrade { get; private set; }
        public Cost GymWorkerStorageUpgrade { get; private set; }
        public Cost GymMovespeedUpgrade { get; private set; }
        public Cost GymBerryGatherSpeedUpgrade { get; private set; }
        public Cost GymMetalGatherSpeedUpgrade { get; private set; }
        public Cost GymSlimeGatherSpeedUpgrade { get; private set; }
        public Cost GymWoodGatherSpeedUpgrade { get; private set; }
        private void Awake()
        {
            WarehouseStorageUpgrade = warehouseUpgrades.Clone("Storage");
            GymWorkerStorageUpgrade = gymUpgrades.Clone("Storage");
            GymMovespeedUpgrade = gymUpgrades.Clone("Movespeed");
            GymBerryGatherSpeedUpgrade = gymUpgrades.Clone("Berry_Gather_Speed");
            GymMetalGatherSpeedUpgrade = gymUpgrades.Clone("Metal_Gather_Speed");
            GymSlimeGatherSpeedUpgrade = gymUpgrades.Clone("Slime_Gather_Speed");
            GymWoodGatherSpeedUpgrade = gymUpgrades.Clone("Wood_Gather_Speed");
            
            BuyableActions.WarehouseActions.TryAdd("Storage", UpgradeWarehouseStorage);
            BuyableActions.GymActions.TryAdd("Storage", UpgradeWorkerStorage);
            BuyableActions.GymActions.TryAdd("Movespeed", UpgradeMovementspeed);
            BuyableActions.GymActions.TryAdd("Berry_Gather_Speed", UpgradeBerryGatherSpeed);
            BuyableActions.GymActions.TryAdd("Metal_Gather_Speed", UpgradeMetalGatherSpeed);
            BuyableActions.GymActions.TryAdd("Slime_Gather_Speed", UpgradeSlimeGatherSpeed);
            BuyableActions.GymActions.TryAdd("Wood_Gather_Speed", UpgradeWoodGatherSpeed);
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
        private void UpgradeWorkerStorage(float x, float y)
        {
            Vector2Int gridPosition = GridUtilities.WorldPositionToGridPosition(new Vector3(x, y));
            Gym gym = EntityContainer.Upgraders[gridPosition] as Gym;
            
            if (gym == null)
            {
                Debug.LogError($"No gym found at position {gridPosition}");
                return;
            }
            
            Cost cost = GymWorkerStorageUpgrade;
            Cost nextCost = new Cost(GymWorkerStorageUpgrade);
            storage.Spend(cost);
            GymWorkerStorageUpgrade = new Cost()
            {
                wood = nextCost.wood * 2,
                berry = nextCost.berry * 2,
                slime = nextCost.slime * 2,
                metal = nextCost.metal * 2
            };
            gym.UpgradeWorkerStorage();
        }
        private void UpgradeMovementspeed(float x, float y)
        {
            Vector2Int gridPosition = GridUtilities.WorldPositionToGridPosition(new Vector3(x, y));
            Gym gym = EntityContainer.Upgraders[gridPosition] as Gym;
            
            if (gym == null)
            {
                Debug.LogError($"No gym found at position {gridPosition}");
                return;
            }
            
            Cost cost = GymMovespeedUpgrade;
            Cost nextCost = new Cost(GymMovespeedUpgrade);
            storage.Spend(cost);
            GymMovespeedUpgrade = new Cost()
            {
                wood = nextCost.wood * 2,
                berry = nextCost.berry * 2,
                slime = nextCost.slime * 2,
                metal = nextCost.metal * 2
            };
            gym.UpgradeMovementspeed();
        }
        private void UpgradeBerryGatherSpeed(float x, float y)
        {
            Vector2Int gridPosition = GridUtilities.WorldPositionToGridPosition(new Vector3(x, y));
            Gym gym = EntityContainer.Upgraders[gridPosition] as Gym;
            
            if (gym == null)
            {
                Debug.LogError($"No gym found at position {gridPosition}");
                return;
            }
            
            Cost cost = GymBerryGatherSpeedUpgrade;
            Cost nextCost = new Cost(GymBerryGatherSpeedUpgrade);
            storage.Spend(cost);
            GymBerryGatherSpeedUpgrade = new Cost()
            {
                wood = nextCost.wood * 2,
                berry = nextCost.berry * 2,
                slime = nextCost.slime * 2,
                metal = nextCost.metal * 2
            };
            gym.UpgradeBerryGatherSpeed();
        }
        private void UpgradeMetalGatherSpeed(float x, float y)
        {
            Vector2Int gridPosition = GridUtilities.WorldPositionToGridPosition(new Vector3(x, y));
            Gym gym = EntityContainer.Upgraders[gridPosition] as Gym;
            
            if (gym == null)
            {
                Debug.LogError($"No gym found at position {gridPosition}");
                return;
            }
            
            Cost cost = GymMetalGatherSpeedUpgrade;
            Cost nextCost = new Cost(GymMetalGatherSpeedUpgrade);
            storage.Spend(cost);
            GymMetalGatherSpeedUpgrade = new Cost()
            {
                wood = nextCost.wood * 2,
                berry = nextCost.berry * 2,
                slime = nextCost.slime * 2,
                metal = nextCost.metal * 2
            };
            gym.UpgradeMetalGatherSpeed();
        }
        private void UpgradeSlimeGatherSpeed(float x, float y)
        {
            Vector2Int gridPosition = GridUtilities.WorldPositionToGridPosition(new Vector3(x, y));
            Gym gym = EntityContainer.Upgraders[gridPosition] as Gym;
            
            if (gym == null)
            {
                Debug.LogError($"No gym found at position {gridPosition}");
                return;
            }
            
            Cost cost = GymSlimeGatherSpeedUpgrade;
            Cost nextCost = new Cost(GymSlimeGatherSpeedUpgrade);
            storage.Spend(cost);
            GymSlimeGatherSpeedUpgrade = new Cost()
            {
                wood = nextCost.wood * 2,
                berry = nextCost.berry * 2,
                slime = nextCost.slime * 2,
                metal = nextCost.metal * 2
            };
            gym.UpgradeSlimeGatherSpeed();
        }
                
        private void UpgradeWoodGatherSpeed(float x, float y)
        {
            Vector2Int gridPosition = GridUtilities.WorldPositionToGridPosition(new Vector3(x, y));
            Gym gym = EntityContainer.Upgraders[gridPosition] as Gym;
            
            if (gym == null)
            {
                Debug.LogError($"No gym found at position {gridPosition}");
                return;
            }
            
            Cost cost = GymWoodGatherSpeedUpgrade;
            Cost nextCost = new Cost(GymWoodGatherSpeedUpgrade);
            storage.Spend(cost);
            GymWoodGatherSpeedUpgrade = new Cost()
            {
                wood = nextCost.wood * 2,
                berry = nextCost.berry * 2,
                slime = nextCost.slime * 2,
                metal = nextCost.metal * 2
            };
            gym.UpgradeWoodGatherSpeed();
        }
    }
}