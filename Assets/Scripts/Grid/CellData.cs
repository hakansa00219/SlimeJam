using Map.Tiles;
using UnityEngine;

namespace Grid
{
    public struct CellData
    {
        public Vector2Int Position;
        public BaseTileType Type;
        public TileElementType ElementType;
        public InteractableTileType InteractableType;
    }
}