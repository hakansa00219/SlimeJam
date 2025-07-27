using System;
using System.Collections.Generic;
using Map.Tiles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "Tile Textures", menuName = "Scriptable/Tile Textures")]
    public class TileTextures : SerializedScriptableObject
    {
        public Dictionary<BaseTileType , Sprite> baseTiles = new Dictionary<BaseTileType, Sprite>();
        public Dictionary<TileElementType , Sprite> elementTiles = new Dictionary<TileElementType, Sprite>();
        public Sprite deleteTileSprite;
        
    }

}
