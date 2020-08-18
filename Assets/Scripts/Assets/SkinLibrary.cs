using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using XRWorld.Core;
using XRWorld.Core.Tiles;

namespace XRWorld.Assets
{
    [CreateAssetMenu(fileName = "TileLibrary", menuName = "XR-World/TileLibrary", order = 1)]    
    public class SkinLibrary : ScriptableObject
    {
        [SerializeField] private LevelData.SkinType _skinType;
        public LevelData.SkinType SkinType
        {
            get { return _skinType; }
        }
        //TODO: Editor tool, edit enum based on skinType
        [Tooltip("Make sure the array order follows the declared enum order")] [SerializeField]
        private Material[] _groundTypes;

        public PlaceableObjectCollection[] placeableObjects;
        
        [SerializeField] private TileEffect[] _tileEffects;

        public Material GetMaterial(int indexID)
        {
            return _groundTypes[indexID];
        }

        // we can't serialize dictionaries in scriptable objects, hence we're iterating.
        public bool HasTileEffects(TileData.GroundType groundType)
        {
            for (int i = 0; i < _tileEffects.Length; i++)
            {
                if (_tileEffects[i].groundType != groundType) continue;

                return true;
            }

            return false;
        }

        public GameObject GetParticleEffect(TileData.GroundType groundType)
        {
            for (int i = 0; i < _tileEffects.Length; i++)
            {
                if (_tileEffects[i].groundType != groundType) continue;

                return _tileEffects[i].ParticleSystemGameObject;
            }
            return null;
        }

    }
    [Serializable]
    public struct TileEffect
    {
        public TileData.GroundType groundType;
        public GameObject ParticleSystemGameObject;
    }
} 


