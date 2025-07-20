using UnityEngine;

namespace Entity
{
    public interface ISpawner
    {
        public void Spawn(Transform prefab, int x, int y);
    }
}