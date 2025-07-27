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
        Wood = 3,
        Metal = 4,
        Berry = 5,
        Slime = 6,
        Flag = 7,
        House = 8,
        Barracks = 9,
        Blacksmith = 10,
        Gym = 11,
        Warehouse = 12,
        Base = 255,
    }
}