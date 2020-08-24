using System.Collections.Generic;
using UnityEngine;

namespace XRWorld.Core.Tiles
{
    public class TileCollection : MonoBehaviour
    {
        private List<Tile> _tiles = new List<Tile>();

        public void AddTile(Tile tile)
        {
            _tiles.Add(tile);
        }

        public Tile GetTileByID(int id)
        {
            return _tiles[id];
        }

        public int Length => _tiles.Count;
    }
}