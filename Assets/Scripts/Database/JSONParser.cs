using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XRWorld.Core;

namespace XRWorld.Database
{
    public class JSONParser : MonoBehaviour
    {
        public TextAsset database;
        public LevelData ParseJSONToLevelData()
        {
            return JsonUtility.FromJson<LevelData>(database.ToString());
        }
    }
}

