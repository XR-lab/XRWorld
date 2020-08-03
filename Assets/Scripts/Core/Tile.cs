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
        private MoneySystem moneySystem;

        public Vector3 PlaceableObjectSpawnPoint
        {
            get { return _placeableObjectSpawnpoint.position; }
        }

        private Renderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            moneySystem = GameObject.Find ("AR Session Origin").GetComponent<MoneySystem> ();
        }
        public void SetGroundData(TileData.GroundType groundType, TileLibrary tileLibrary)
        {
            _tileData.groundType = groundType;
            _renderer.material = tileLibrary.GetMaterial((int)groundType);
        }
        public void SetTileData(TileData tileData, TileLibrary tileLibrary)
        {
            _tileData = tileData;
            _renderer.material = tileLibrary.GetMaterial((int)tileData.groundType);
        }
        public void SetObjectData(PlaceableObjectData placeAbleObjectData, TileLibrary tileLibrary)
        {
            //summon plant
            print(_tileData.HasPlaceableObject);
            if (!_tileData.HasPlaceableObject && moneySystem.playerMoney >= placeAbleObjectData.cost)
            {
                _tileData.placeableObjectData = placeAbleObjectData;
                
                print(_tileData.HasPlaceableObject);
                moneySystem.RemovePlayerMoney(placeAbleObjectData.cost);
                Vector3 spawnableObjectPosition = PlaceableObjectSpawnPoint;
                PlaceableObjectCollection collection =
                    tileLibrary.placeableObjects[_tileData.placeableObjectData.id];
                GameObject objectToSpawn = collection.GetGameObjectByLevel(_tileData.placeableObjectData.level);
                    
                Instantiate(objectToSpawn, spawnableObjectPosition,
                    Quaternion.identity, _placeableObjectSpawnpoint.transform);
            }
            else
            {
                Debug.Log("Not Enough Money");
            }
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
        

        public bool HasPlaceableObject
        {
            get { return placeableObjectData.id > -1; }
        }
    }

    [Serializable]
    public struct PlaceableObjectData
    {
        public int cost;
        public int id;
        public int level;
        public string placedBy;
        public string timeStamp;
    }
}

