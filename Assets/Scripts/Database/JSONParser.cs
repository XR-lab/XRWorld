using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XRWorld.Core;

namespace XRWorld.Database
{
    public class JSONParser : MonoBehaviour
    {
        public TextAsset database;

        void Start()
        {
            LevelData data = JsonUtility.FromJson<LevelData>(database.ToString());

            // TODO: fix direct reference to LevelSpawner
            GetComponent<LevelSpawner>().SpawnLevel(data);
        }
    }
}

