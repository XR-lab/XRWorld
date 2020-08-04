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
        public int cost;
        //public TileData.GroundType groundType;
       // public PlaceableObjectData objectToPlace;
        private void Awake()
        {
            _tile = GetComponent<Tile>();
           

        }
        
        public void ChangeGroundType(int groundTypeID)
        {
            TileData.GroundType groundType = (TileData.GroundType)groundTypeID;
            _tile = _tileSelector._selectedTile;
            _tile.SetGroundData(groundType,_tileLibrary);

        }

        public void SetPlaceableObject(int objectToPlaceIndex)
        {
            // TODO: Fix timestamp and read username
            PlaceableObjectData newObject = new PlaceableObjectData();
            
            newObject.id = objectToPlaceIndex;
            newObject.level = 0;
            newObject.cost = cost;
            newObject.placedBy = "User name";
            _tile = _tileSelector._selectedTile;
            _tile.SetObjectData( newObject,_tileLibrary);
            

        }

    }
}