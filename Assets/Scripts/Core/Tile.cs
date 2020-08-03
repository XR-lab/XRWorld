using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XRWorld.Database;

namespace XRWorld.Core
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private Transform _placeableObjectSpawnpoint;
        
        // data visualized for debuggin purpose
        [SerializeField] private TileData _tileData;

        public TileData TileData => _tileData;

        public Vector3 PlaceableObjectSpawnPoint
        {
            get { return _placeableObjectSpawnpoint.position; }
        }

        private Renderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        public void SetTileData(TileData tileData)
        {
            _tileData = tileData;
            
            SetGroundType(_tileData.groundType);

            if (_tileData.placeableObjectData.id > -1)
            {
                AddPlaceableObject(_tileData.placeableObjectData.id);
            }
        }

        public void SetGroundType(TileData.GroundType newType)
        {
            var tileLibrary = SkinResources.Instance.GetTileLibrary();
            _tileData.groundType = newType;
            _renderer.material = tileLibrary.GetMaterial((int)_tileData.groundType);
        }
        
        public void AddPlaceableObject(int placeableObjectID)
        {
            Vector3 spawnableObjectPosition = _placeableObjectSpawnpoint.position;
            TileLibrary tileLibrary = SkinResources.Instance.GetTileLibrary();
            PlaceableObjectCollection collection = tileLibrary.placeableObjects[placeableObjectID];
            GameObject objectToSpawn = collection.GetGameObjectByLevel(_tileData.placeableObjectData.level);
            
            Instantiate(objectToSpawn, spawnableObjectPosition, Quaternion.identity, transform);

            _tileData.placeableObjectData.id = placeableObjectID;
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
        public int progress;
        public string placedBy;
        public string timeStamp;
    }
}

