using UnityEngine;
using XRWorld.Core;

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
        
        public Material GetMaterial(int indexID)
        {
            return _groundTypes[indexID];
        }
    } 
    } 


