using System;
using Firebase.Database;
using UnityEngine;
using XRWorld.Core.Tiles;

namespace XRWorld.Database
{
    public class LevelChangeHandler : MonoBehaviour
    {
        private DatabaseReference _reference;
        void Start()
        {
            _reference = FirebaseDatabase.DefaultInstance.GetReference(DatabaseConstants.TILES_REF);
        }

        public void ParseGroundTypeChange(Tile tile, TileData.GroundType newGroundType)
        {
            string tileReference = String.Concat(tile.ID, "/" ,DatabaseConstants.GROUNDTYPE);
            _reference.Child(tileReference).SetValueAsync((int)newGroundType);
        }

        public void ParsePlaceableObjectPlacement(Tile tile, PlaceableObjectData data)
        {
            string tileID = tile.ID.ToString();
            _reference.Child(tileID).Child(DatabaseConstants.PLACEABLE_OBJECT_DATA).SetRawJsonValueAsync(JsonUtility.ToJson(data));
        }
    }
}

