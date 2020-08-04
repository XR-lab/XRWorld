using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XRWorld.Core;

namespace XRWorld.Database
{
    // not using this class. could be deleted in time...
    public class JSONParser : MonoBehaviour
    {
        public TextAsset database;
        public LevelData ParseJSONToLevelData()
        {
            return JsonUtility.FromJson<LevelData>(database.ToString());
        }
        
        
    }
}

