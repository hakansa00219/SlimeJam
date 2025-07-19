using System;
using Map.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map
{
    [RequireComponent(typeof(Tilemap))]
    public class TileManager : MonoBehaviour
    {
        private Tilemap _tilemap;

        private void Awake()
        {
            _tilemap = GetComponent<Tilemap>();
        }

        private BaseTile GetTile(byte x, byte y)
        {
            Vector3Int tilePosition = new Vector3Int(x, y);
            BaseTile tile = _tilemap.GetTile<BaseTile>(tilePosition);
            return tile;
        }

        // Remove Tile element
        public void RemoveTileElement(byte x, byte y)
        {
            BaseTile tile = GetTile(x, y);

            if (tile == null)
            {
                Debug.LogWarning($"No tile found at position ({x}, {y}).");
                return;
            }

            tile.elementType = TileElementType.Empty;
        }
        
        // Change Tile Element
        public void ChangeTileElement(byte x, byte y, TileElementType elementType)
        {
            BaseTile tile = GetTile(x, y);

            if (tile == null)
            {
                Debug.LogWarning($"No tile found at position ({x}, {y}).");
                return;
            }
            
            tile.elementType = elementType;
        }
        
    }
}

