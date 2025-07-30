using Map.Tiles;

namespace Entity
{
    public interface IStorage
    {
        public StorageInfo CurrentInfo { get; set; }
        public StorageCapacity Capacity { get; set; }
        
        public struct StorageInfo
        {
            public int Total => Wood + Metal + Slime + Berry;
            public int Wood;
            public int Metal;
            public int Slime;
            public int Berry;

            public StorageInfo(int wood, int metal, int slime, int berry)
            {
                Wood = wood;
                Metal = metal;
                Slime = slime;
                Berry = berry;
            }

            public int GetCurrentType(TileElementType materialType)
            {
                return materialType switch
                {
                    TileElementType.Berry => Berry,
                    TileElementType.Wood => Wood,
                    TileElementType.Metal => Metal,
                    TileElementType.Slime => Slime
                };
            }
            
        }
        
        public struct StorageCapacity
        {
            public int Total;
            public int Wood;
            public int Metal;
            public int Slime;
            public int Berry;

            public StorageCapacity(int total, int wood, int metal, int slime, int berry)
            {
                Total = total;
                Wood = wood;
                Metal = metal;
                Slime = slime;
                Berry = berry;
            }
            
            public int GetCurrentType(TileElementType materialType)
            {
                return materialType switch
                {
                    TileElementType.Berry => Berry,
                    TileElementType.Wood => Wood,
                    TileElementType.Metal => Metal,
                    TileElementType.Slime => Slime
                };
            }
        }
    }
}