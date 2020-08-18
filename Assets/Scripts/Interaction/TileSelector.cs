using UnityEngine;
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
        [SerializeField] private float _uiShowTime = 0.2f;

        [Header("UI Elements")]
        [SerializeField] private RectTransform _selectPanel; //first panel
        public RectTransform _objectPanel; //object build panel
        public RectTransform _selectPanelWithObject; 
        public RectTransform _tilePanel;   //change tile texture panel
        
        private float _uiHeightOffset;
        private Tile _selectedTile;
        public Tile SelectedTile => _selectedTile;
        
        private RectTransform _currentPanel;
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

                    LeanTween.moveY(_selectedTile.gameObject, _selectedTile.transform.position.y + _heightSelection,
                        _selectionSpeed).setEaseInBack();
                   
                    _currentPanel.localScale = Vector3.zero;
                    LeanTween.scale(_currentPanel, Vector3.one * 0.01f, _uiShowTime).setEase(LeanTweenType.easeInCubic);
                    CheckHeightOffset();
                    _currentPanel.transform.position = new Vector3(_selectedTile.transform.position.x , _selectedTile.transform.position.y + _uiHeightOffset,_selectedTile.transform.position.z - 3 );
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
            CheckHeightOffset();
            Vector3 position = new Vector3(_selectedTile.transform.position.x , _selectedTile.transform.position.y + _uiHeightOffset,_selectedTile.transform.position.z - 3 );
            panel.transform.position = position;
            panel.gameObject.SetActive(true);
        }
        private void CloseAllUIPanels()
        {
            _selectPanel.gameObject.SetActive(false);
            _tilePanel.gameObject.SetActive(false);
            _objectPanel.gameObject.SetActive(false);
            _selectPanelWithObject.gameObject.SetActive(false);
        }

        private void CheckHeightOffset()
        {
            int id = _selectedTile.GetPlaceableObjectID();
            if (id == 3)
            {
                _uiHeightOffset = 6;
            }
            else
            {
                _uiHeightOffset = 4;
            }
        }
    }   
}

