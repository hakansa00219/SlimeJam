using System;
using System.Collections.Generic;
using Map.Tiles;
using Structure;

namespace UI
{
    public static class BuyableActions
    {
        public static readonly Dictionary<string, Action<float,float>> BuildingActions =
            new Dictionary<string, Action<float,float>>();
        public static readonly Dictionary<string, Action<float, float>> WarehouseActions =
            new Dictionary<string, Action<float, float>>();
        public static readonly Dictionary<string, Action<float,float>> RemoveActions =
            new Dictionary<string, Action<float,float>>();
    }
}