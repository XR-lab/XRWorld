using UnityEngine;
using UnityEngine.UI;
using XRWorld.Core;

namespace XRWorld.Interaction
{
    public class TileSelector : MonoBehaviour
    {
        [SerializeField] private float _heightSelection = 1;
        [SerializeField] private Button _button;
        
        public Tile _selectedTile;
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
                    _selectedTile = tile;
                    _selectedTile.transform.position = new Vector3(_selectedTile.transform.position.x, _selectedTile.transform.position.y + _heightSelection, 
                        _selectedTile.transform.position.z);
                    _button.transform.position = new Vector3(_selectedTile.transform.position.x + 1, _selectedTile.transform.position.y + 1,_selectedTile.transform.position.z );
                    _button.gameObject.SetActive(true);
                    
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
                _button.gameObject.SetActive(false);
            }
        }
    }   
}

