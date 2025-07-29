namespace Data
{
    public class Cost
    {
        public int Wood;
        public int Metal; 
        public int Slime;
        public int Berry;
        public int TotalCost => Wood + Metal + Slime + Berry;
        public Cost() {}
        public Cost(int wood, int metal, int slime, int berry)
        {
            Wood = wood;
            Metal = metal;
            Slime = slime;
            Berry = berry;
        }
    }
}