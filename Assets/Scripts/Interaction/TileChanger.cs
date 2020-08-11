﻿using System;
using UnityEngine;
using XRWorld.Core;
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
        private PlaceableObjectData newData;

        private LevelChangeHandler _levelChangeHandler;

        private void Start()
        {
            _levelChangeHandler = FindObjectOfType<LevelChangeHandler>();

        }

        public void OnLevelLoaded()
        {
            
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
            newData.id = objectToPlaceIndex;
            newData.level = 1;
            newData.placedBy = "TEST User name";
            newData.progress = 0;
            Debug.Log(newData.id);

            newData.timeStamp = DateTime.Now.ToString();
            
            
            _tile = _tileSelector._selectedTile;

            _levelChangeHandler.ParsePlaceableObjectPlacement(_tile, newData);
        }
        
        public void SetPlaceableObjectLevel(int level)
        {
            _tile = _tileSelector._selectedTile;
            
            // TODO: Fix username
            newData.id = _tile.CheckId();
            newData.level = level;
            newData.placedBy = "TEST User name";
            newData.progress = 0;
            newData.timeStamp = DateTime.Now.ToString();
            

            _levelChangeHandler.ParsePlaceableObjectPlacement(_tile, newData);
        }
    }
}