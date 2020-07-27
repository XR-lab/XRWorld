using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XRWorld.Core;
using XRWorld.Database;
using XRWorld.Interaction;


namespace XRWorld.Interaction
{
    public class TileChanger : MonoBehaviour
    {
        [SerializeField] private TileLibrary _tileLibrary;
        private Renderer _renderer;

        private TileSelector _tileSelector;
        private Tile _tile;
        public TileData _newTile;
        private void Awake()
        {
            _tileSelector = GetComponent<TileSelector>();
            _tile = GetComponent<Tile>();
        }

        public void ChangeTile()
        {
             
            _tile = _tileSelector._selectedTile;
            _tile.SetTileData(_newTile,_tileLibrary);
            

            

        }

    }
}