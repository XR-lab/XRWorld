using Firebase.Database;
using UnityEngine;
using XRWorld.Core.Cameras;
using XRWorld.Users;
using Random = UnityEngine.Random;

namespace XRWorld.Database
{
    public class CameraChangeHandler : MonoBehaviour
    {
        private DatabaseReference _reference;
        private CameraCollection _collection;
        private string _sessionId;
        private string _sesionNick = "Tester";
        private CamData data;
        
        void Start()
        {
            _reference = FirebaseDatabase.DefaultInstance.GetReference(DatabaseConstants.PLAYERS);
            _sessionId = Random.Range(0, 1000000).ToString();
            _collection = FindObjectOfType<CameraCollection>();
            _collection.SetSessionID(_sessionId);
            _sesionNick = FindObjectOfType<NickNameSetter>().GetNickName();
        }

        public void AddCameraDataToDatabase(Vector3 pos, Vector3 rot)
        {
            data = new CamData(_sessionId, _sesionNick, pos,rot);
            _reference.Child(_sessionId +" "+ _sesionNick).SetRawJsonValueAsync(JsonUtility.ToJson(data));
        }

        public void ParseUpdatedCameraData(Vector3 pos, Vector3 rot)
        {
            data.SetPosRot(pos, rot);
            _reference.Child(_sessionId +" "+ _sesionNick).SetRawJsonValueAsync(JsonUtility.ToJson(data));
        }

        private void OnApplicationQuit()
        {
            _reference.Child(_sessionId +" "+ _sesionNick).SetRawJsonValueAsync(null);
        }
    }
}

