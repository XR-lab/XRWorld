﻿using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;
using XRWorld.Assets;

namespace XRWorld.Core.Tiles
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private Transform _placeableObjectSpawnpoint;
        
        [SerializeField] private GameObject _spawnEffectPrefab;
        [SerializeField] private float _spawnEffectTime = 0.5f;
        [SerializeField] private GameObject _trailEffect;
        
        private TileData _tileData;
        public TileData TileData => _tileData;
        
        private TimeKeeper _timeKeeper;
        
        private int _ID;
        public int ID => _ID;
        
        public bool HasPlaceableObject => _tileData.placeableObjectData.id > -1;

        private GameObject tileEffect;
        private Renderer _renderer;
        private Transform _placeableObject;
        
        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _timeKeeper = GetComponentInParent<TimeKeeper>();
        }
        
        public void SetTileData(TileData tileData, int tileID)
        {
            _ID = tileID;
            _tileData = tileData;
            
            SetGroundType(_tileData.groundType);

            if (HasPlaceableObject)
            {
                int tempLevel = _timeKeeper.CheckAge(_tileData.placeableObjectData, _ID);
                AddPlaceableObject(_tileData.placeableObjectData.id, tempLevel, false);
            }
        }

        public void SetGroundType(TileData.GroundType newType)
        {
            _tileData.groundType = newType;
            var tileLibrary = SkinResources.Instance.GetTileLibrary();
            _renderer.material = tileLibrary.GetMaterial((int)_tileData.groundType);
            
            if (tileEffect != null)
                Destroy(tileEffect);
            
            if (tileLibrary.HasTileEffects(_tileData.groundType))
            {
                GameObject effect = tileLibrary.GetParticleEffect(_tileData.groundType);
                tileEffect = Instantiate(effect, transform);
            }
            
        }

        private void AddPlaceableObject(int placeableObjectID, int placeableObjectLevel, bool showSpawnFX = true)
        {
            _tileData.placeableObjectData.id = placeableObjectID;
            _tileData.placeableObjectData.level = placeableObjectLevel;
            
            
            Vector3 spawnableObjectPosition = _placeableObjectSpawnpoint.position;
            SkinLibrary skinLibrary = SkinResources.Instance.GetTileLibrary();
            PlaceableObjectCollection collection = skinLibrary.placeableObjects[_tileData.placeableObjectData.id];
            GameObject objectToSpawn = collection.GetGameObjectByLevel(_tileData.placeableObjectData.level);
            
            _placeableObject = Instantiate(objectToSpawn, spawnableObjectPosition, Quaternion.identity, transform).transform;
            _placeableObject.gameObject.GetComponent<Renderer>().material.SetFloat("_SpawnProgress", 0);

            if (showSpawnFX)
            {
                Instantiate(_spawnEffectPrefab, transform);
                StartPlacableObjectSpawnEffect();
            }
                
        }

        public void OnTilePlaced()
        {
            if (HasPlaceableObject)
                StartPlacableObjectSpawnEffect();

            Destroy(_trailEffect);
        }
        
        private void StartPlacableObjectSpawnEffect()
        {
            LeanTween.value(_placeableObject.gameObject, UpdateSpawnEffect, 0f, 1f, _spawnEffectTime).setEaseOutCubic();
        }
        void UpdateSpawnEffect(float val)
        {
            _placeableObject.gameObject.GetComponent<Renderer>().material.SetFloat("_SpawnProgress", val);
        }
        
        public void ReplacePlaceableObject(int placeableObjectID, int placeableObjectLevel)
        {
            RemovePlaceableObject(); 
            AddPlaceableObject(placeableObjectID, placeableObjectLevel);
        }

       public int GetPlaceableObjectID()
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

       public void SetTimeStamp(string timeStamp)
       {
           _tileData.placeableObjectData.timeStamp = timeStamp;
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

