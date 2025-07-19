using Map.Tiles;
using Unity.Mathematics;

namespace Grid
{
    public struct CellData
    {
        public int2 Position;
        public BaseTileType Type;
        public TileElementType ElementType;
    }
}