using System;
using Entity;
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
        [VerticalGroup("Buildings"), SerializeField] private Transform warehousePrefab;

        private void Awake()
        {
            BuyableActions.BuildingActions.TryAdd(StructureTileType.Warehouse, BuildWarehouse);
        }

        [Button]
        public void BuildWarehouse(float x, float y)
        {
            spawner.Spawn(warehousePrefab, x, y);
        }
    }
}