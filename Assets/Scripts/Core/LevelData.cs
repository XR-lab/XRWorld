using UnityEngine;
using XRWorld.Core.Tiles;

namespace XRWorld.Core
{
    [System.Serializable]
    public struct LevelData
    {
        public enum SkinType
        {
            Garden
        }

        public SkinType skinType;
        public TileData[] tiles;
        
        public Vector2 GetMaxLevelSize()
        {
            Vector2 size = Vector2.zero;

            foreach (var tileData in tiles)
            {
                if (tileData.posX > size.x)
                    size.x = tileData.posX;
                if (tileData.posZ > size.y)
                    size.y = tileData.posZ;
            }

            return size;
        }
    }
}