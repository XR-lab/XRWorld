using Firebase.Database;
using UnityEngine;
using XRWorld.Core.Cameras;
using Random = UnityEngine.Random;

namespace XRWorld.Database
{
    public class CameraChangeHandler : MonoBehaviour
    {
        private DatabaseReference _reference;
        private CameraCollection _collection;
        private string _sessionId; public string GetSessionID() {return _sessionId;}
        private CamData data;
        
        void Start()
        {
            _reference = FirebaseDatabase.DefaultInstance.GetReference(DatabaseConstants.PLAYERS);
            _sessionId = Random.Range(0, 1000000).ToString();
            _collection = FindObjectOfType<CameraCollection>();
            _collection.SetSessionID(_sessionId);
        }

        public void AddCameraDataToDatabase(Vector3 pos, Vector3 rot)
        {
            data = new CamData(_sessionId,pos,rot);
            _reference.Child(_sessionId).SetRawJsonValueAsync(JsonUtility.ToJson(data));
        }

        public void ParseUpdatedCameraData(Vector3 pos, Vector3 rot)
        {
            data.SetPosRot(_sessionId, pos, rot);
            _reference.Child(_sessionId).SetRawJsonValueAsync(JsonUtility.ToJson(data));
        }

        private void OnApplicationQuit()
        {
            //_reference.Child(_sessionId).SetRawJsonValueAsync(null);
        }
    }
    
    
}

