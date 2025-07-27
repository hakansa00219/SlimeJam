using System;
using Entity;
using Entity.Entities;
using Grid;
using Map.Tiles;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace Structure
{
    public class BuildingSystem : MonoBehaviour
    {
        [SerializeField] private EntitySpawner spawner;
        [SerializeField] private GridManager gridManager;
        [VerticalGroup("Buildings"), SerializeField] private Transform warehousePrefab;

        private void Awake()
        {
            BuyableActions.BuildingActions.TryAdd("Warehouse", BuildWarehouse);
            BuyableActions.BuildingActions.TryAdd("Road", BuildRoad);
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
            BuyableActions.RemoveActions.TryAdd("Road", gridManager.RemoveRoad);
        }

        [Button]
        public void BuildWarehouse(float x, float y)
        {
            spawner.Spawn(warehousePrefab, x, y);
        }
        [Button]
        public void BuildRoad(float x, float y)
        {
            gridManager.CreateRoad(x, y);
        }

    }
}