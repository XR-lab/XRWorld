using UnityEngine;

namespace XRWorld.Database
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

    [System.Serializable]
    public struct TileData
    {
        public enum GroundType
        {
            Earth,
            Grass,
            Water
        }

        public GroundType groundType;
        public int posX;
        public int posZ;
        public PlaceableObjectData placeableObjectData;
    }

    [System.Serializable]
    public struct PlaceableObjectData
    {
        public int id;
        public int level;
        public string placedBy;
        public string timeStamp;
    }
}