using System.Collections.Generic;
using UnityEngine;
using XRWorld.Core;
using XRWorld.Core.Tiles;

namespace XRWorld.Assets
{
    public class SkinResources : MonoBehaviour
    {
        [SerializeField] private LevelData.SkinType _currentSkinType;
    
        public static SkinResources Instance;

        [SerializeField] private SkinLibrary[] _tileLibraries;
        private Dictionary<LevelData.SkinType, SkinLibrary> _resourcesMap;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            _resourcesMap = new Dictionary<LevelData.SkinType, SkinLibrary>();

            for (int i = 0; i < _tileLibraries.Length; i++)
            {
                _resourcesMap.Add(_tileLibraries[i].SkinType, _tileLibraries[i]);
            }
        }

        public SkinLibrary GetTileLibrary()
        {
            return _resourcesMap[_currentSkinType];
        }
    }
}

