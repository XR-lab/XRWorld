using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using XRWorld.Core;

public class SkinResources : MonoBehaviour
{
    [SerializeField] private LevelData.SkinType _currentSkinType;
    
    public static SkinResources Instance;

    [SerializeField] private TileLibrary[] _tileLibraries;
    private Dictionary<LevelData.SkinType, TileLibrary> _resourcesMap;
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
        _resourcesMap = new Dictionary<LevelData.SkinType, TileLibrary>();

        for (int i = 0; i < _tileLibraries.Length; i++)
        {
            _resourcesMap.Add(_tileLibraries[i].SkinType, _tileLibraries[i]);
        }
    }

    public TileLibrary GetTileLibrary()
    {
        return _resourcesMap[_currentSkinType];
    }
}
