using System;
using UnityEngine;
using XRWorld.Core.Tiles;
using XRWorld.Database;

namespace XRWorld.Interaction
{
    public class TileChanger : MonoBehaviour
    {
        private Renderer _renderer;
        public TileSelector _tileSelector;
        private Tile _tile;
        public int cost;

        private LevelChangeHandler _levelChangeHandler;

        private void Start()
        {
            _levelChangeHandler = FindObjectOfType<LevelChangeHandler>();
        }

        public void ChangeGroundType(int groundTypeID)
        {
            TileData.GroundType groundType = (TileData.GroundType)groundTypeID;
            _tile = _tileSelector._selectedTile;

           _levelChangeHandler.ParseGroundTypeChange(_tile, groundType);
        }

        public void SetPlaceableObject(int objectToPlaceIndex)
        {
            // TODO: Fix username
            PlaceableObjectData newData = new PlaceableObjectData();
            newData.id = objectToPlaceIndex;
            newData.level = 1;
            newData.placedBy = "TEST User name";
            newData.progress = 0;
            newData.timeStamp = DateTime.Now.ToString();
            
            _tile = _tileSelector._selectedTile;

            _levelChangeHandler.ParsePlaceableObjectPlacement(_tile, newData);
        }
        
        public void SetPlaceableObjectLevel(int level)
        {
            // TODO: Fix username
            PlaceableObjectData newData = new PlaceableObjectData();
         
            newData.level = level;
            newData.timeStamp = DateTime.Now.ToString();
            
            _tile = _tileSelector._selectedTile;

            _levelChangeHandler.ParsePlaceableObjectPlacement(_tile, newData);
        }
    }
}