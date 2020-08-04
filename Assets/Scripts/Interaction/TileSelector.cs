using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using XRWorld.Core.Tiles;

namespace XRWorld.Interaction
{
    public class TileSelector : MonoBehaviour
    {
        [SerializeField] private float _heightSelection = 1;
        [SerializeField] private RectTransform _tilePanel;
        [SerializeField] private RectTransform _objectPanel;
        
        public Tile _selectedTile;
        private TileData _tileData;
        private RectTransform _currentPanel;
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
                    _currentPanel = tile.HasPlaceableObject ? _objectPanel : _tilePanel;
                    UnselectedTile(tile);
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

