using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XRWorld.Core.Tiles;
using XRWorld.Database;

namespace XRWorld.Core
{
    public class TimeKeeper : MonoBehaviour
    {
        [SerializeField] private int daysToDecay, hoursToDecay, minutesToDecay;
        private TimeSpan _decayTime;

        private List<Tile> _tileCollection;
        private LevelChangeHandler _levelChangeHandler;
        
        //code logic
        private bool getTiles = true;
        
        public void Start()
        {
            _decayTime = new TimeSpan(daysToDecay,hoursToDecay,minutesToDecay, 0);
            _levelChangeHandler = FindObjectOfType<LevelChangeHandler>();
            StartCoroutine(Loop());
        }

        private IEnumerator Loop()
        {
            while (true)
            {
                yield return new WaitForSeconds(30);
                print("Yes");
                FindAllPlacedObjects();
            }
        }

        private void FindAllPlacedObjects()
        {
            if(getTiles){_tileCollection = FindObjectOfType<TileCollection>().GetAllTiles();}

            for (int i = 0; i < _tileCollection.Count; i++)
            {
                if (_tileCollection[i].HasPlaceableObject)
                {
                    CheckAge(_tileCollection[i].TileData.placeableObjectData, i);
                }
            }
            
        }

        public int CheckAge(PlaceableObjectData data, int ID)
        {
            int level = data.level;
            if (level == 0) return level;
            
            DateTime original = DateTime.Parse(data.timeStamp);
            DateTime currentTime = DateTime.Now;
            TimeSpan diff = currentTime - original;

            if (diff.TotalMinutes > _decayTime.TotalMinutes)
            {
                print(diff.TotalMinutes);
                print(_decayTime.TotalMinutes);
                level--;
                if (level != 0 && diff.TotalMinutes > (_decayTime.TotalMinutes + _decayTime.TotalMinutes))
                {
                    level--;
                }

                data.level = level;
                data.timeStamp = DateTime.Now.ToString();
                _levelChangeHandler.ParsePlaceableObjectPlacement(ID.ToString(), data);
            }

            return level;
        }
    }
}