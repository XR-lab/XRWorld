using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Unity.Editor;
using UnityEngine;
using UnityEngine.Events;
using XRWorld.Core;

public class FirebaseLevelLoader : MonoBehaviour
{
    [Serializable]
    public class UnityLevelDataEvent : UnityEvent<LevelData>
    {
    }
    
    public UnityLevelDataEvent OnLevelLoaded;
    private const string LEVEL_KEY = "LEVEL_KEY";
    private const string TILES_REF = "LEVEL_KEY/tiles";

    private DatabaseReference _reference;
    private DatabaseReference _tilesReference;

    private TileCollection _tileCollection;
    private void Awake()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://xr-world.firebaseio.com/");
        _reference = FirebaseDatabase.DefaultInstance.RootReference;
        _tilesReference = FirebaseDatabase.DefaultInstance.GetReference(TILES_REF);
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
        FirebaseDatabase.DefaultInstance.GetReference(LEVEL_KEY).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("couldn't load level from database");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                LevelData levelData = JsonUtility.FromJson<LevelData>(snapshot.GetRawJsonValue());
                
                Debug.Log("Level Loaded");
                OnLevelLoaded.Invoke(levelData);
            }
        });
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
        } 
        if (currentData.placeableObjectData.id == -1 && newData.placeableObjectData.id > -1)
        {
            // instantiate the new placeable object
            changedTile.AddPlaceableObject(newData.placeableObjectData.id);
        }
    }
}