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

        public TileSelector _tileSelector;
        private Tile _tile;
        public TileData _newTile;
        private void Awake()
        {
            _tile = GetComponent<Tile>();
           

        }
        
        public void ChangeTileGrass()
        {
            _newTile.groundType = TileData.GroundType.Grass;
            _tile = _tileSelector._selectedTile;
            _tile.SetTileData(_newTile,_tileLibrary);

        }
        public void ChangeTileStone()
        {
            _newTile.groundType = TileData.GroundType.Stone;
            _tile = _tileSelector._selectedTile;
            _tile.SetTileData(_newTile,_tileLibrary);

        }
        public void ChangeTileWater()
        {
            _newTile.groundType = TileData.GroundType.Water;
            _tile = _tileSelector._selectedTile;
            _tile.SetTileData(_newTile,_tileLibrary);

        }
        public void SetPlant()
        {
            _newTile.placeableObjectData.id = 1;
            _newTile.placeableObjectData.level = 1;
            _tile = _tileSelector._selectedTile;
            _tile.SetObjectData(_newTile.placeableObjectData,_tileLibrary);

        }

    }
}