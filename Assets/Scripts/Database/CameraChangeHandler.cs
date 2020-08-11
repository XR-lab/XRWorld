using Firebase.Database;
using UnityEngine;
using Random = UnityEngine.Random;

namespace XRWorld.Database
{
    public class CameraChangeHandler : MonoBehaviour
    {
        private DatabaseReference _reference;
        private string _sessionId;
        private CamData data;
        
        void Start()
        {
            _reference = FirebaseDatabase.DefaultInstance.GetReference(DatabaseConstants.PLAYERS);
            _sessionId = Random.Range(0, 1000000).ToString();
        }

        public void AddCameraDataToDatabase(Vector3 pos, Vector3 rot)
        {
            data = new CamData(pos,rot);
            _reference.Child(_sessionId).SetRawJsonValueAsync(JsonUtility.ToJson(data));
        }

        public void ParseUpdatedCameraData(Vector3 pos, Vector3 rot)
        {
            data.SetPosRot(pos, rot);
            _reference.Child(_sessionId).SetRawJsonValueAsync(JsonUtility.ToJson(data));
        }

        private void OnApplicationQuit()
        {
            _reference.Child(_sessionId).SetRawJsonValueAsync(null);
        }
    }
    
    public class CamData
    {
        public Vector3Int pos, rot;
        public CamData(Vector3 pos, Vector3 rot)
        {
            SetPosRot(pos, rot);
        }
        public void SetPosRot(Vector3 pos, Vector3 rot)
        {
            this.pos = new Vector3Int(Mathf.FloorToInt(pos.x * 100),
                        Mathf.FloorToInt(pos.y * 100),
                        Mathf.FloorToInt(pos.z * 100));
            this.rot = new Vector3Int((Mathf.FloorToInt(rot.x * 100)) / 100,
                       (Mathf.FloorToInt(rot.y * 100)) / 100,
                       (Mathf.FloorToInt(rot.z * 100)) / 100);
        }
    }
}

