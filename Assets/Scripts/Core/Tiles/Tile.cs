using System;
using UnityEngine;
using XRWorld.Assets;

namespace XRWorld.Core.Tiles
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private Transform _placeableObjectSpawnpoint;
        [SerializeField] private Transform _placeableObject;
        
        // data visualized for debuggin purpose
        [SerializeField] private TileData _tileData;
        
        
        private int _ID;
        public int ID
        {
            get { return _ID; }
        }
        public TileData TileData => _tileData;
   
        public bool HasPlaceableObject
        {
            get { return _tileData.placeableObjectData.id > -1; }
        }

        public Vector3 PlaceableObjectSpawnPoint
        {
            get { return _placeableObjectSpawnpoint.position; }
        }

        private Renderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        public void SetTileData(TileData tileData, int tileID)
        {
            _ID = tileID;
            _tileData = tileData;
            
            SetGroundType(_tileData.groundType);

            if (HasPlaceableObject)
            {
                AddPlaceableObject(_tileData.placeableObjectData.id, _tileData.placeableObjectData.level);
            }
        }

 
        public void SetGroundType(TileData.GroundType newType)
        {
            _tileData.groundType = newType;
            var tileLibrary = SkinResources.Instance.GetTileLibrary();
            _renderer.material = tileLibrary.GetMaterial((int)_tileData.groundType);
            
            if (tileLibrary.HasTileEffects(_tileData.groundType))
            {
                GameObject effect = tileLibrary.GetParticleEffect(_tileData.groundType);
                Instantiate(effect, transform);
            }
            
        }

        private void AddPlaceableObject(int placeableObjectID, int placeableObjectLevel)
        {
            _tileData.placeableObjectData.id = placeableObjectID;
            _tileData.placeableObjectData.level = placeableObjectLevel;
            
            Vector3 spawnableObjectPosition = _placeableObjectSpawnpoint.position;
            SkinLibrary skinLibrary = SkinResources.Instance.GetTileLibrary();
            PlaceableObjectCollection collection = skinLibrary.placeableObjects[_tileData.placeableObjectData.id];
            GameObject objectToSpawn = collection.GetGameObjectByLevel(_tileData.placeableObjectData.level);
            
            _placeableObject = Instantiate(objectToSpawn, spawnableObjectPosition, Quaternion.identity, transform).transform;
        }

        public void ReplacePlaceableObject(int placeableObjectID, int placeableObjectLevel)
        {
            RemovePlaceableObject();
            AddPlaceableObject(placeableObjectID, placeableObjectLevel);
        }

       public int CheckId()
        {
            int id = _tileData.placeableObjectData.id;
            return id;
        }

       public void RemovePlaceableObject()
       {
           if (HasPlaceableObject)
           {
               _tileData.placeableObjectData.id = -1;
               Destroy(_placeableObject.gameObject);    
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

