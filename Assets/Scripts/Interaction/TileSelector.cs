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
        
        [Header("UI Elements")]
        [SerializeField] private RectTransform _selectPanel; //first panel
        public RectTransform _objectPanel; //object build panel
        public RectTransform _selectPanelWithObject; 
        public RectTransform _tilePanel;   //change tile texture panel

        public float uiHeight;
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
                    CheckforHeight();
                    _currentPanel.transform.position = new Vector3(_selectedTile.transform.position.x , _selectedTile.transform.position.y + uiHeight,_selectedTile.transform.position.z  );
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
            CheckforHeight();
            panel.transform.position = new Vector3(_selectedTile.transform.position.x , _selectedTile.transform.position.y + uiHeight,_selectedTile.transform.position.z  );
                panel.gameObject.SetActive(true);
                
        }
        
        private void CloseAllUIPanels()
        {
            _selectPanel.gameObject.SetActive(false);
            _tilePanel.gameObject.SetActive(false);
            _objectPanel.gameObject.SetActive(false);
            _selectPanelWithObject.gameObject.SetActive(false);
        }

        private void CheckforHeight()
        {
            //TODO make sure that it works for all skins and not only (id == 3)
            int id;
            id = _selectedTile.CheckId();
            Debug.Log(id);
            if (id == 3)
            {
                uiHeight = 6;
            }
            else
            {
                uiHeight = 4;
            }

        }
    }
}

