namespace Map.Tiles
{
    public enum BaseTileType : byte
    {
        Grass = 0,
        Water = 1,
        Dirt = 2,
        Sand = 3
    }

    public enum TileElementType : byte
    {
        Empty = 0,
        Road = 1,
        Rock = 2,
        Main = 255,
    }
    
    public enum InteractableTileType : byte
    {
        Empty = 0,
        Wood = 1,
        Metal = 2,
        Berry = 3,
        Slime = 4,
        Flag = 5,
    }

    public enum StructureTileType : byte
    {
        Empty = 0,
        House = 1,
        Barracks = 2,
        Blacksmith = 3,
        Gym = 4,
    }
}