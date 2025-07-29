using Data;
using Entity;
using Entity.Entities;
using Grid;
using Scriptable;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using Utility;

namespace Structure
{
    public class BuildingSystem : MonoBehaviour
    {
        [SerializeField] private EntitySpawner spawner;
        [SerializeField] private GridManager gridManager;
        [SerializeField] private Costs buildings;
        [SerializeField] private Storage storage;
        [VerticalGroup("Buildings"), SerializeField] private Transform warehousePrefab;
        [VerticalGroup("Buildings"), SerializeField] private Transform gymPrefab;

        private void Awake()
        {
            BuyableActions.BuildingActions.TryAdd("Warehouse", BuildWarehouse);
            BuyableActions.BuildingActions.TryAdd("Road", BuildRoad);
            BuyableActions.BuildingActions.TryAdd("Gym", BuildGym);
            BuyableActions.RemoveActions.TryAdd("Warehouse",
                (x, y) =>
                {
                    Vector2Int position = GridUtilities.WorldPositionToGridPosition(new Vector3(x, y));

                    if (EntityContainer.Depositables.TryGetValue(position, out IDepositable structure))
                    {
                        if (structure is Warehouse warehouse)
                            Destroy(warehouse.gameObject);
                        EntityContainer.Depositables.Remove(position);
                    }
                    else
                    {
                        Debug.LogError($"No structure found at position {position}");
                    }
                });
            BuyableActions.RemoveActions.TryAdd("Gym",
                (x, y) =>
                {
                    Vector2Int position = GridUtilities.WorldPositionToGridPosition(new Vector3(x, y));

                    if (EntityContainer.Upgraders.TryGetValue(position, out IUpgrader upgrader))
                    {
                        if (upgrader is Gym gym)
                            Destroy(gym.gameObject);
                        EntityContainer.Upgraders.Remove(position);
                    }
                    else
                    {
                        Debug.LogError($"No structure found at position {position}");
                    }
                });
            BuyableActions.RemoveActions.TryAdd("Road", gridManager.RemoveRoad);
        }

        [Button]
        public void BuildWarehouse(float x, float y)
        {
            Cost cost = buildings.Clone("Warehouse");
            storage.Spend(cost);
            spawner.Spawn(warehousePrefab, x, y);
        }
        [Button]
        public void BuildRoad(float x, float y)
        {
            Cost cost = buildings.Clone("Road");
            storage.Spend(cost);
            gridManager.CreateRoad(x, y);
        }

        [Button]
        public void BuildGym(float x, float y)
        {
            Cost cost = buildings.Clone("Gym");
            storage.Spend(cost);
            spawner.Spawn(gymPrefab, x, y);
        }

    }
}