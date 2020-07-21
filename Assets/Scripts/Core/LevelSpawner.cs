using UnityEngine;
using XRWorld.Database;

namespace XRWorld.Core
{
    public class LevelSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private TileLibrary _tileLibrary;

        [SerializeField] private float _maxHeightOffset;
        [SerializeField] private float _heightTiers;
        
        public void SpawnLevel(LevelData levelData)
        {
            float heightSample = _maxHeightOffset / (_heightTiers - 0);
            float heightStep = 1f / _heightTiers;
            
            Vector2 levelSize = GetMaxLevelSize(levelData);
            
            foreach (var tileData in levelData.tiles)
            {
                float yOffset = Mathf.PerlinNoise(tileData.posX / levelSize.x, tileData.posZ / levelSize.y);
                
                for (int i = 0; i < _heightTiers; i++)
                {
                    if (yOffset >= heightStep * i && yOffset < heightStep * (i+1))
                    {
                        yOffset = heightSample * i;
                    }
                }
                
                Vector3 spawnPosition = new Vector3(tileData.posX, yOffset, tileData.posZ);
                GameObject tile = Instantiate(_tilePrefab, spawnPosition, Quaternion.identity, transform);
                Renderer renderer = tile.GetComponent<Renderer>();
                renderer.material.color = _tileLibrary.GetColor((int)tileData.groundType);
                
                if (tileData.placeableObjectData.id > -1)
                {
                    //TODO: calculate y-offset (or save in library)
                    Vector3 spawnableObjectPosition = new Vector3(tileData.posX, spawnPosition.y+0.75f, tileData.posZ);
                    Instantiate(_tileLibrary.placeableObjects[tileData.placeableObjectData.id], spawnableObjectPosition,
                        Quaternion.identity, tile.transform);
                }
            }

            transform.position =  new Vector3(transform.position.x - levelSize.x / 2f, transform.position.y, transform.position.z - levelSize.y / 2f);
        }

        private Vector2 GetMaxLevelSize(LevelData levelData)
        {
            Vector2 size = Vector2.zero;

            foreach (var tileData in levelData.tiles)
            {
                if (tileData.posX > size.x)
                    size.x = tileData.posX;
                if (tileData.posZ > size.y)
                    size.y = tileData.posZ;
            }

            return size;
        }
    }
}

