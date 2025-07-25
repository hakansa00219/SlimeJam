using System;
using System.Collections.Generic;
using Map.Tiles;

namespace UI
{
    public static class BuyableActions
    {
        public static Dictionary<StructureTileType, Action<float,float>> BuildingActions =
            new Dictionary<StructureTileType, Action<float,float>>();
    }
}