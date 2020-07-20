using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XRWorld.Database;

namespace XRWorld.Gameplay
{
    public class LevelSpawner : MonoBehaviour
    {
        public void SpawnLevel(LevelData levelData)
        {
            foreach (var tileData in levelData.tiles)
            {
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = new Vector3(tileData.posX, 0, tileData.posZ);
                obj.transform.SetParent(transform);
            }
        }
    }
}

