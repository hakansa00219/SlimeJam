using UnityEngine;

namespace Entity
{
    public abstract class Spawner : MonoBehaviour
    {
        [SerializeField]
        protected Transform container;
    }
}