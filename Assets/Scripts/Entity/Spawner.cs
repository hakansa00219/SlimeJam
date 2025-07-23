using UnityEngine;
using UnityEngine.Tilemaps;

namespace Entity
{
    public abstract class Spawner : MonoBehaviour
    {
        [SerializeField]
        protected Transform container;
        [SerializeField]
        protected Tilemap overlayTilemap;
    }
}