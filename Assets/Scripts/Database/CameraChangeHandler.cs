using System;
using Firebase.Database;
using UnityEngine;

namespace XRWorld.Database
{
    public class CameraChangeHandler : MonoBehaviour
    {
        private DatabaseReference _reference;
        void Start()
        {
            _reference = FirebaseDatabase.DefaultInstance.GetReference(DatabaseConstants.TILES_REF);
        }

        public void AddCameraDataToDatabase()
        {
            //_reference
        }
        
    }
}

