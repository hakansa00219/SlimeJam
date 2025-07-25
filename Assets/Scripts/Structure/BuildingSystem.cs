using System;
using Entity;
using Grid;
using Map.Tiles;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

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