using UnityEngine.Tilemaps;

namespace Entity
{
    public interface IEntity
    {
        public void Initialize(Tilemap overlayTilemap, int x, int y);
    }
}