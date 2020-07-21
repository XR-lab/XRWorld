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

