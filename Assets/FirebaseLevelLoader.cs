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

    private DatabaseReference _reference;
    private void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://xr-world.firebaseio.com/");
        _reference = FirebaseDatabase.DefaultInstance.RootReference;
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
}
