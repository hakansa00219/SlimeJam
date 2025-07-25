using Map.Tiles;
using UnityEngine;

namespace Grid
{
    public struct CellData
    {
        public BaseTileType Type;
        public TileElementType ElementType;
        public InteractableTileType InteractableType;
        public StructureTileType StructureType;
    }
}