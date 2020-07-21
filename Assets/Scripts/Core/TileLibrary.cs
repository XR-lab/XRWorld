using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XRWorld.Database;

namespace XRWorld.Core
{
    [CreateAssetMenu(fileName = "TileLibrary", menuName = "TileLibrary", order = 1)]
    public class TileLibrary : ScriptableObject
    {
        [SerializeField] private LevelData.SkinType _skinType;
        
        //TODO: Editor tool, edit enum based on skinType
        //TODO: convert Color to material
        [Tooltip("Make sure the array order follows the declared enum order")]
        [SerializeField] private Color[] _GroundTypes;

        public GameObject[] placeableObjects;
        
        public Color GetColor(int indexID)
        {
            return _GroundTypes[indexID];
        }

    } 
}

