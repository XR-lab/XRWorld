using System.Collections.Generic;
using UnityEngine;

namespace XRWorld.Core.Cameras
{
    public class CameraCollection : MonoBehaviour
    {
        private Dictionary<string, Transform> _camCollection = new Dictionary<string, Transform>();
        private string _sessionID; public void SetSessionID(string ID) {_sessionID = ID;}
        private Vector3 _theMiddle;

        [SerializeField] private GameObject _camera;

        public void SetTheMiddle(Vector3 mid)
        {
            _theMiddle = mid;
        }
        
        public void CameraUpdate(CamData data)
        {
            if(data.name == _sessionID) 
                return;

            if (!_camCollection.ContainsKey(data.name))
            {
                NewCamera(data);
            }

            MoveCam(data);
        }

        private void NewCamera(CamData data)
        {
            Transform tra = Instantiate(_camera).GetComponent<Transform>();
            _camCollection.Add(data.name, tra);
        }

        private void MoveCam(CamData data)
        {
            Vector3 pos = new Vector3(data.pos.x, data.pos.y,data.pos.z) / 100;
            _camCollection[data.name].position = pos + _theMiddle;
        }
    }
}

