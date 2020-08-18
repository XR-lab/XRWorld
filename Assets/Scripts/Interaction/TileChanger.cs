using System;
using UnityEngine;
using XRWorld.Core;
using XRWorld.Core.Tiles;
using XRWorld.Database;

namespace XRWorld.Interaction
{
    public class TileChanger : MonoBehaviour
    {
        private Renderer _renderer;
        public TileSelector _tileSelector;
        private Tile _tile;
        public int cost;
        private PlaceableObjectData newData;

        private LevelChangeHandler _levelChangeHandler;

        private void Start()
        {
            _levelChangeHandler = FindObjectOfType<LevelChangeHandler>();

        }
        

        public void ChangeGroundType(int groundTypeID)
        {
            TileData.GroundType groundType = (TileData.GroundType)groundTypeID;
            _tile = _tileSelector.SelectedTile;

           _levelChangeHandler.ParseGroundTypeChange(_tile, groundType);
        }

        public void SetPlaceableObject(int objectToPlaceIndex)
        {
            // TODO: Fix username
            newData.id = objectToPlaceIndex;
            newData.level = 1;
            newData.placedBy = "TEST User name";
            newData.progress = 0;
            Debug.Log(newData.id);

            newData.timeStamp = DateTime.Now.ToString();
            
            _tile = _tileSelector.SelectedTile;


            _levelChangeHandler.ParsePlaceableObjectPlacement(_tile, newData);
        }

        public void SetPlaceableObjectLevel(int level)
        {
            _tile = _tileSelector.SelectedTile;
    
            // TODO: Fix username
            newData.id = _tile.CheckId();
            newData.level = level;
            newData.placedBy = "TEST User name";
            newData.progress = 0;
            newData.timeStamp = DateTime.Now.ToString();
            
            _levelChangeHandler.ParsePlaceableObjectPlacement(_tile, newData);
        }


        public void NextPanel(int panelId)
        {
            if (panelId == 1)
            {
                _tileSelector.OpenTilePanel();
                /*
                _tileSelector._currentPanel = _tileSelector._tilePanel;
                
                _tileSelector.CurrentPanel.gameObject.SetActive(false);
                _tileSelector.CurrentPanel.transform.position = new Vector3(_tileSelector.SelectedTile.transform.position.x , _tileSelector.SelectedTile.transform.position.y + 2,_tileSelector.SelectedTile.transform.position.z - 3 );

                _tileSelector.CurrentPanel.gameObject.SetActive(true);
                */
            }
            else
            {
                _tileSelector.OpenObjectPanel();
                /*
                _tileSelector._currentPanel = _tileSelector._objectPanel;
                _tileSelector.CurrentPanel.gameObject.SetActive(false);
               
                _tileSelector.CurrentPanel.transform.position = new Vector3(_tileSelector.SelectedTile.transform.position.x , _tileSelector.SelectedTile.transform.position.y + 2,_tileSelector.SelectedTile.transform.position.z - 3 );

                _tileSelector.CurrentPanel.gameObject.SetActive(true);
                */
            }

            
        }
    }
}