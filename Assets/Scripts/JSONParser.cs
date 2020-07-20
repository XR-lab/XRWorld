using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XRWorld.Gameplay;

namespace XRWorld.Database
{
    public class JSONParser : MonoBehaviour
    {
        public TextAsset database;

        void Start()
        {
            LevelData data = JsonUtility.FromJson<LevelData>(database.ToString());

            // TODO: fix direct reference to LevelSpawner
            GetComponent<LevelSpawner>().SpawnLevel(data);
        }
    }

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

