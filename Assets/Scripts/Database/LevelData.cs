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