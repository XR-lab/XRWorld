using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XRWorld.Core;

namespace XRWorld.Interaction
{
    public class TileSelector : MonoBehaviour
    {
        [SerializeField] private float _heightSelection = 1;
        
        private Tile _selectedTile;

        private void Start()
        {
            _selectedTile = null;
        }


        public void SelectTile(Tile tile)
        { 
            if (tile != null)
            {
                if (tile == _selectedTile)
                {
                    UnselectedTile(tile);
                }
                else
                {
                    UnselectedTile(tile);
                    _selectedTile = tile;
                    _selectedTile.transform.position = new Vector3(_selectedTile.transform.position.x, _selectedTile.transform.position.y + _heightSelection, 
                        _selectedTile.transform.position.z);
                }
            }
        }

        private void UnselectedTile(Tile tile)
        {
            if (_selectedTile != null)
            {
                _selectedTile.transform.position = new Vector3(_selectedTile.transform.position.x, _selectedTile.transform.position.y - _heightSelection, 
                    _selectedTile.transform.position.z);
                _selectedTile = null;
            }
        }
    }   
}

