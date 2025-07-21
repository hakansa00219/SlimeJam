using Entity;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Worker
{
    public class Worker : MonoBehaviour, IEntity
    {
        public int GridPositionX { get; set; }
        public int GridPositionY { get; set; }
        public Tilemap OverlayTilemap { get; private set; }

        public void Initialize(Tilemap overlayTilemap, int x, int y)
        {
            OverlayTilemap = overlayTilemap;
            GridPositionX = x;
            GridPositionY = y;
        } 
    }
}