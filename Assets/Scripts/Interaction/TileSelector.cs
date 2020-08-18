﻿using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using XRWorld.Core.Tiles;

namespace XRWorld.Interaction
{
    public class TileSelector : MonoBehaviour
    {
        [Header("Selection")]
        [SerializeField] private float _heightSelection = 1;
        [SerializeField] private float _selectionSpeed = 0.1f;
        
        [Header("UI Elements")]
        [SerializeField] private RectTransform _selectPanel; //first panel
        public RectTransform _objectPanel; //object build panel
        public RectTransform _selectPanelWithObject; 
        public RectTransform _tilePanel;   //change tile texture panel
        
        private Tile _selectedTile;
        public Tile SelectedTile => _selectedTile;
        private RectTransform _currentPanel;
        public RectTransform CurrentPanel => _currentPanel;

        private TileData _tileData;
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
                    _currentPanel = tile.HasPlaceableObject ? _selectPanelWithObject : _selectPanel;
                    _selectedTile = tile;

                    LeanTween.moveY(_selectedTile.gameObject, _selectedTile.transform.position.y + _heightSelection, _selectionSpeed);

                    _currentPanel.transform.position = new Vector3(_selectedTile.transform.position.x , _selectedTile.transform.position.y + 2,_selectedTile.transform.position.z - 3 );
                    _currentPanel.gameObject.SetActive(true);
                    
                }
            }
        }
        
        private void UnselectedTile(Tile tile)
        {
            if (_selectedTile != null)
            {
                LeanTween.moveY(_selectedTile.gameObject, _selectedTile.transform.position.y - _heightSelection, _selectionSpeed);
        
                _selectedTile = null;
                CloseAllUIPanels();
            }
        }

        public void OpenTilePanel()
        {
            CloseAllUIPanels();
            
            PlaceAndEnableUIPanel(_tilePanel);
        }

        public void OpenObjectPanel()
        {
            CloseAllUIPanels();

            PlaceAndEnableUIPanel(_objectPanel);
        }

        private void PlaceAndEnableUIPanel(RectTransform panel)
        {
            panel.transform.position = new Vector3(_selectedTile.transform.position.x , _selectedTile.transform.position.y + 2,_selectedTile.transform.position.z - 3 );
            panel.gameObject.SetActive(true);
        }
        private void CloseAllUIPanels()
        {
            _selectPanel.gameObject.SetActive(false);
            _tilePanel.gameObject.SetActive(false);
            _objectPanel.gameObject.SetActive(false);
            _selectPanelWithObject.gameObject.SetActive(false);
        }
    }   
}

