using UnityEngine;

namespace XRWorld.Core
{
    public class LevelSpawner : MonoBehaviour
    {
        [SerializeField] private Tile _tilePrefab;
        [SerializeField] private TileLibrary _tileLibrary;

        [SerializeField] private float _maxHeightOffset = 0.25f;
        [SerializeField] private float _heightTiers = 4;
        
        public void SpawnLevel(LevelData levelData, Vector3 placementPosition, Quaternion placementRotation)
        {
            float scaledHeightStep = _maxHeightOffset / (_heightTiers - 1);
            float unscaledHeightStep = 1f / _heightTiers;
            Vector2 levelSize = levelData.GetMaxLevelSize();
 
            foreach (var tileData in levelData.tiles)
            {
                float yOffset = Mathf.PerlinNoise(tileData.posX / levelSize.x, tileData.posZ / levelSize.y);
                
                for (int i = 0; i < _heightTiers; i++)
                {
                    if (yOffset >= unscaledHeightStep * i && yOffset < unscaledHeightStep * (i+1))
                    {
                        yOffset = scaledHeightStep * i;
                    }
                }
                
                Vector3 spawnPosition = new Vector3(tileData.posX, yOffset, tileData.posZ);
                Tile tile = Instantiate(_tilePrefab, spawnPosition, Quaternion.identity, transform);
                tile.SetTileData(tileData, _tileLibrary);
                
                if (tileData.placeableObjectData.id > -1)
                {
                    Vector3 spawnableObjectPosition = tile.PlaceableObjectSpawnPoint;
                    Instantiate(_tileLibrary.placeableObjects[tileData.placeableObjectData.id], spawnableObjectPosition,
                        Quaternion.identity, tile.transform);
                }
            }

            // adjust spawner position offset, to center spawnpostion of level. Might not need this in AR.
            transform.position = placementPosition;
            transform.rotation = placementRotation;
        }
    }
}

