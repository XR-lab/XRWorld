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

        [Header("Spawn Effect Settings")]
        [SerializeField] private float _spawnSphereSize = 20;
        [SerializeField] private float _minAssembleSpeed;
        [SerializeField] private float _maxAssembleSpeed;
        
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
                
                Vector3 spawnPosition = Random.insideUnitSphere * _spawnSphereSize;
                Vector3 levelPosition = new Vector3(tiles[i].posX, yOffset, tiles[i].posZ);
                Tile tile = Instantiate(_tilePrefab, spawnPosition, Quaternion.identity, transform);
                tile.SetTileData(tiles[i], i);

                float tweenTime = Random.Range(_minAssembleSpeed, _maxAssembleSpeed);
                LeanTween.moveLocal(tile.gameObject, levelPosition, tweenTime).setEase(LeanTweenType.easeInBack);
                if (tile.HasPlaceableObject)
                    LeanTween.rotateAround(tile.gameObject, Vector3.up, 360, tweenTime)
                        .setOnComplete(tile.OnTilePlaced);
                else
                    LeanTween.rotateAround(tile.gameObject, Vector3.up, 360, tweenTime);
                
                _tileCollection.AddTile(tile);
            }

            // adjust spawner position offset, to center spawnpostion of level. Might not need this in AR.
            transform.position = placementPosition;
            transform.rotation = placementRotation;
        }
    }
}

