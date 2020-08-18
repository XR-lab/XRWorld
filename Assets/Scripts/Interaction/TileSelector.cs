using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using XRWorld.Core.Tiles;

namespace XRWorld.Interaction
{
    public class TileSelector : MonoBehaviour
    {
        [SerializeField] private float _heightSelection = 1;
        [SerializeField] private RectTransform _selectPanel; //first panel
        public RectTransform _objectPanel; //object build panel
        public RectTransform _selectPanelWithObject; 
        public RectTransform _tilePanel;   //change tile texture panel
        public Tile _selectedTile;
        private TileData _tileData;
        public RectTransform _currentPanel;
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
                    _selectedTile.transform.position = new Vector3(_selectedTile.transform.position.x, _selectedTile.transform.position.y + _heightSelection, 
                        _selectedTile.transform.position.z);
                    _currentPanel.transform.position = new Vector3(_selectedTile.transform.position.x , _selectedTile.transform.position.y + 2,_selectedTile.transform.position.z - 3 );
                    _currentPanel.gameObject.SetActive(true);
                    
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
                _currentPanel.gameObject.SetActive(false);
            }
        }
    }   
}

