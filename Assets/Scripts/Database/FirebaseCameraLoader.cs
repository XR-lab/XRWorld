using UnityEngine;
using Firebase.Database;
using XRWorld.Core.Cameras;

namespace XRWorld.Database
{
    public class FirebaseCameraLoader : MonoBehaviour
    {
        private DatabaseReference _reference;
        private CameraCollection _camCollection;
    
        public void LevelLoadedStart()
        { 
            _reference = FirebaseDatabase.DefaultInstance.GetReference(DatabaseConstants.PLAYERS);
            _camCollection = FindObjectOfType<CameraCollection>();
            Enable();
        }
    
        private void Enable()
        {
            _reference.ChildChanged += HandleChildChanged;
        }

        private void OnDisable()
        {
            _reference.ChildChanged -= HandleChildChanged;
        }

        private void HandleChildChanged(object sender, ChildChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                Debug.LogError(args.DatabaseError.Message);
                return;
            }

            CamData incomingData = JsonUtility.FromJson<CamData>(args.Snapshot.GetRawJsonValue());
            _camCollection.CameraUpdate(incomingData);
        }
    }
}
