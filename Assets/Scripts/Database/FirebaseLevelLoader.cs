using System;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Unity.Editor;
using UnityEngine;
using UnityEngine.Events;
using XRWorld.Core;
using XRWorld.Core.Tiles;

namespace XRWorld.Database
{
    public class FirebaseLevelLoader : MonoBehaviour
    {
        [SerializeField] private bool _sendLocalDatabaseToServer;
        [SerializeField] private TextAsset _databaseFile;

        [Serializable]
        public class UnityLevelDataEvent : UnityEvent<LevelData>
        {
        }

        public UnityLevelDataEvent OnLevelLoaded;

        private DatabaseReference _reference;
        private DatabaseReference _tilesReference;

        private TileCollection _tileCollection;

        private void Awake()
        {
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(DatabaseConstants.DB_URL);
            _reference = FirebaseDatabase.DefaultInstance.RootReference;
            _tilesReference = FirebaseDatabase.DefaultInstance.GetReference(DatabaseConstants.TILES_REF);
        }

        private void Start()
        {
            _tileCollection = FindObjectOfType<TileCollection>();
        }

        private void OnEnable()
        {
            _tilesReference.ChildChanged += HandleChildChanged;
        }

        private void OnDisable()
        {
            _tilesReference.ChildChanged -= HandleChildChanged;
        }

        public void LoadLevelData()
        {
            FirebaseDatabase.DefaultInstance.GetReference(DatabaseConstants.LEVEL_KEY).GetValueAsync()
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsFaulted)
                    {
                        Debug.Log("couldn't load level from database");
                    }
                    else if (task.IsCompleted)
                    {
                        // using this boolean for simple reset of our online database
                        if (!_sendLocalDatabaseToServer)
                        {
                            DataSnapshot snapshot = task.Result;
                            LevelData levelData = JsonUtility.FromJson<LevelData>(snapshot.GetRawJsonValue());

                            Debug.Log("Level Loaded");
                            OnLevelLoaded.Invoke(levelData);
                        }
                        else
                        {
                            SendLocalDatabaseToServer();
                        }
                    }
                });
        }

        private void SendLocalDatabaseToServer()
        {
            LevelData levelData = JsonUtility.FromJson<LevelData>(_databaseFile.ToString());
            _reference.Child(DatabaseConstants.LEVEL_KEY).SetRawJsonValueAsync(JsonUtility.ToJson(levelData));
        }

        // TODO: Filter the changed data to make specific changes related to the tiles
        void HandleChildChanged(object sender, ChildChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                Debug.LogError(args.DatabaseError.Message);
                return;
            }

            TileData newData = JsonUtility.FromJson<TileData>(args.Snapshot.GetRawJsonValue());
            Tile changedTile = _tileCollection.GetTileByID(Int32.Parse(args.Snapshot.Key));
            TileData currentData = changedTile.TileData;

            if (currentData.groundType != newData.groundType)
            {
                //changedTile.SetTileData(newData);
                changedTile.SetGroundType(newData.groundType);
                Debug.Log("Change From Server");
            }

            if (currentData.placeableObjectData.id == -1 && newData.placeableObjectData.id > -1)
            {
                // instantiate the new placeable object
                changedTile.AddPlaceableObject(newData.placeableObjectData.id, newData.placeableObjectData.level);
            }
        }
    }
}
