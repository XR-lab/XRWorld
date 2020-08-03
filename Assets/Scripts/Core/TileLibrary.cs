using System.Runtime.InteropServices;
using UnityEngine;
using XRWorld.Database;

namespace XRWorld.Core
{
    [CreateAssetMenu(fileName = "TileLibrary", menuName = "XR-World/TileLibrary", order = 1)]    
    public class TileLibrary : ScriptableObject
    {
        [SerializeField] private LevelData.SkinType _skinType;
        
        //TODO: Editor tool, edit enum based on skinType
        [Tooltip("Make sure the array order follows the declared enum order")] [SerializeField]
        private Material[] _groundTypes;

        public PlaceableObjectCollection[] placeableObjects;
        
        public Material GetMaterial(int indexID)
        {
            return _groundTypes[indexID];
        }
    } 
}

