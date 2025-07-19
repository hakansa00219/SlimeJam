using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map.Tiles 
{
    [CreateAssetMenu(fileName = "New Tile", menuName = "Tiles/New Tile")]
    public abstract class BaseTile : Tile
    {
        public readonly byte X;
        public readonly byte Y;
        public BaseTileType type;
        public TileElementType elementType;
        
        protected BaseTile (byte x, byte y, BaseTileType type, TileElementType elementType)
        {
            X = x;
            Y = y;
            type = type;
            elementType = elementType;
        }
        
        protected void SetType(BaseTileType type)
        {
            type = type;
        }

        protected void SetElementType(TileElementType elementType)
        {
            elementType = elementType;
        }
    }
}