using UnityEngine;
using XRWorld.Assets;
using XRWorld.Core.Tiles;

namespace XRWorld.Core
{
    [RequireComponent(typeof(TileCollection))]
    public class LevelSpawner : MonoBehaviour
    {
        [SerializeField] private Tile _tilePrefab;
        [SerializeField] private SkinLibrary _tileLibrary;

        [SerializeField] private float _maxHeightOffset = 0.25f;
        [SerializeField] private float _heightTiers = 4;

        private TileCollection _tileCollection;
        private void Start()
        {
            _tileCollection = GetComponent<TileCollection>();
        }

        public void SpawnLevel(LevelData levelData, Vector3 placementPosition, Quaternion placementRotation)
        {
            float scaledHeightStep = _maxHeightOffset / (_heightTiers - 1);
            float unscaledHeightStep = 1f / _heightTiers;
            Vector2 levelSize = levelData.GetMaxLevelSize();
            TileData[] tiles = levelData.tiles;
            
            for (int i = 0; i < tiles.Length; i++)
            {
                float yOffset = Mathf.PerlinNoise(tiles[i].posX / levelSize.x, tiles[i].posZ / levelSize.y);
                
                for (int j = 0; j < _heightTiers; j++)
                {
                    if (yOffset >= unscaledHeightStep * j && yOffset < unscaledHeightStep * (j+1))
                    {
                        yOffset = scaledHeightStep * j;
                    }
                }
                
                Vector3 spawnPosition = new Vector3(tiles[i].posX, yOffset, tiles[i].posZ);
                Tile tile = Instantiate(_tilePrefab, spawnPosition, Quaternion.identity, transform);
                tile.SetTileData(tiles[i], i);

                _tileCollection.AddTile(tile);
            }
            /*
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
                tile.SetTileData(tileData);
                
                _tileCollection.AddTile(tile, tileData);
            }
            */

            // adjust spawner position offset, to center spawnpostion of level. Might not need this in AR.
            transform.position = placementPosition;
            transform.rotation = placementRotation;
        }
    }
}

