using UnityEngine;

namespace Entity
{
    public interface ISpawner
    {
        public Transform Spawn(Transform prefab, int x, int y);
    }
}