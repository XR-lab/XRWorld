using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;
using XRWorld.Core;
using XRWorld.Database;
using XRWorld.Interaction;


namespace XRWorld.Interaction
{
    public class TileChanger : MonoBehaviour
    {
        private Renderer _renderer;
        public TileSelector _tileSelector;
        private Tile _tile;
        public int cost;

        private DatabaseReference _reference;
        //TODO: place database saving in a specific class
        void Start()
        {
            _reference = FirebaseDatabase.DefaultInstance.GetReference("LEVEL_KEY/tiles");
        }
        public void ChangeGroundType(int groundTypeID)
        {
            TileData.GroundType groundType = (TileData.GroundType)groundTypeID;
            _tile = _tileSelector._selectedTile;
            //_tile.SetGroundType(groundType);

            string reference = String.Concat(_tile.ID, "/groundType");
            _reference.Child(reference).SetValueAsync(groundTypeID);
        }

        public void SetPlaceableObject(int objectToPlaceIndex)
        {
            /*
            // TODO: Fix timestamp and read username
            PlaceableObjectData newObject = new PlaceableObjectData();
            
            newObject.id = objectToPlaceIndex;
            newObject.level = 0;
            newObject.cost = cost;
            newObject.placedBy = "User name";
            */
            _tile = _tileSelector._selectedTile;
            _tile.AddPlaceableObject(objectToPlaceIndex, 1);

            
        }
    }
}