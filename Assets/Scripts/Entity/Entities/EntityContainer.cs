using System.Collections.Generic;
using Entity.Entities.Flag;
using Structure;
using UnityEngine;

namespace Entity.Entities
{
    public static class EntityContainer
    {
        public static readonly Dictionary<Vector2Int, IGatherable> Gatherables = new Dictionary<Vector2Int, IGatherable>(); 
        public static readonly Dictionary<Vector2Int, IDepositable> Depositables = new Dictionary<Vector2Int, IDepositable>();
        public static readonly Dictionary<Vector2Int, IUpgrader> Upgraders = new Dictionary<Vector2Int, IUpgrader>();
        public static readonly Dictionary<Vector2Int, IConvertable> Convertables = new Dictionary<Vector2Int, IConvertable>();
        public static readonly Dictionary<Vector2Int, IPurchasable> Purchasables = new Dictionary<Vector2Int, IPurchasable>();
        public static Worker.Worker Worker;

        public static bool CheckWinCondition()
        {
            foreach (var (_, value) in Convertables)
            {
                if (!value.IsConverted) return false;
            }

            return true;
        }
    }
}