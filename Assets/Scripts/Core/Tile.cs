using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XRWorld.Database;

namespace XRWorld.Core
{
    public class Tile : MonoBehaviour
    {
        // data visualized for debuggin purpose
        [SerializeField] private Transform _placeableObjectSpawnpoint;
        [SerializeField] private TileData _tileData;


        public Vector3 PlaceableObjectSpawnPoint
        {
            get { return _placeableObjectSpawnpoint.position; }
        }

        private Renderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        public void SetTileData(TileData tileData, TileLibrary tileLibrary)
        {
            _tileData = tileData;
            _renderer.material = tileLibrary.GetMaterial((int)tileData.groundType);
        }
        public void SetObjectData(PlaceableObjectData placeAbleObjectData, TileLibrary tileLibrary)
        {
            _tileData.placeableObjectData.id = placeAbleObjectData.id;
            //summon plant
         
            Vector3 spawnableObjectPosition = PlaceableObjectSpawnPoint;
            PlaceableObjectCollection collection =
                tileLibrary.placeableObjects[_tileData.placeableObjectData.id];
            GameObject objectToSpawn = collection.GetGameObjectByLevel(_tileData.placeableObjectData.level);
                    
            Instantiate(objectToSpawn, spawnableObjectPosition,
                Quaternion.identity, _placeableObjectSpawnpoint.transform);
            Debug.Log("hi");
        }
    }
    
    [Serializable]
    public struct TileData
    {
        public enum GroundType
        {
            Grass,
            Stone,
            Water
        }

        public GroundType groundType;
        public int posX;
        public int posZ;
        public PlaceableObjectData placeableObjectData;
    }

    [Serializable]
    public struct PlaceableObjectData
    {
        public int id;
        public int level;
        public string placedBy;
        public string timeStamp;
    }
}

