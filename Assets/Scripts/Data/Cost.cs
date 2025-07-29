using System;
using UnityEngine.Serialization;

namespace Data
{
    [Serializable]
    public class Cost
    {
        public int wood;
        public int metal; 
        public int slime;
        public int berry;
        public int TotalCost => wood + metal + slime + berry;
        public Cost() {}

        public Cost(Cost cost)
        {
            wood = cost.wood;
            metal = cost.metal;
            slime = cost.slime;
            berry = cost.berry;
        }
        public Cost(int wood, int metal, int slime, int berry)
        {
            this.wood = wood;
            this.metal = metal;
            this.slime = slime;
            this.berry = berry;
        }
    }
}