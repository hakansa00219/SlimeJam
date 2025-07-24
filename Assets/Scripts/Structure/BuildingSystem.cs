using Entity;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Structure
{
    public class BuildingSystem : MonoBehaviour
    {
        [SerializeField] private EntitySpawner spawner;
        [VerticalGroup("Buildings"), SerializeField] private Transform warehousePrefab;
        [Button]
        public void BuildWarehouse(int x, int y)
        {
            spawner.Spawn(warehousePrefab, x, y);
        }
    }
}