using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.Events;

public class FirebaseInit : MonoBehaviour
{
   public UnityEvent OnFirebaseInitializd = new UnityEvent();
   private void Start()
   { 
      FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
          if (task.Exception != null)
          {
              Debug.LogError($"Failed to initialize Firebase with {task.Exception}");
              return;
          }
          OnFirebaseInitializd.Invoke();
      });
   }
}
