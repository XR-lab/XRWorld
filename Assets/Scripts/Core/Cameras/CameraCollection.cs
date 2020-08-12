using System.Collections;
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
            if(data.name == _sessionID) return;

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
            StartCoroutine(CamLerp(pos + _theMiddle, data.rot, _camCollection[data.name]));
        }

        private float amountOfSteps = 10;
        IEnumerator CamLerp(Vector3 targetPos, Vector3 targetRot, Transform camTransform)
        {
            Vector3 posSteps = (camTransform.position - targetPos) / amountOfSteps;
           
            Vector3 camRot = camTransform.rotation.eulerAngles;
            Vector3 rotSteps = Vector3.zero;
            
            //TODO try to get this to work without summoning a devil that will trow up when it looks at this part of the code
            rotSteps = -((camRot - targetRot) / amountOfSteps);
            if(rotSteps.x > 18 || rotSteps.x < -18){rotSteps.x = (camRot.x>targetRot.x? -((camRot.x-360) - targetRot.x) : -(camRot.x - (targetRot.x-360))) / amountOfSteps;}
            if(rotSteps.y > 18 || rotSteps.y < -18){rotSteps.y = (camRot.y>targetRot.y? -((camRot.y-360) - targetRot.y) : -(camRot.y - (targetRot.y-360))) / amountOfSteps;}
            if(rotSteps.z > 18 || rotSteps.z < -18){rotSteps.z = (camRot.z>targetRot.z? -((camRot.z-360) - targetRot.z) : -(camRot.z - (targetRot.z-360))) / amountOfSteps;}
            
            print(camTransform.rotation.eulerAngles);
            print(targetRot);
            print(rotSteps);
            
            int i = 0;
            while (i < amountOfSteps)
            {
                camTransform.position -= posSteps;
                camTransform.Rotate(rotSteps);
                
                i++;
                yield return new WaitForSeconds(0.5f / amountOfSteps);
            }
            yield return null;
            camTransform.rotation = Quaternion.Euler(targetRot);
        }
    }
}

// Quaternion qRotSteps = new Quaternion(
//     camRot.x > qTargetRot.x ? -(camRot.x - qTargetRot.x) / amountOfSteps : (qTargetRot.x - camRot.x) / amountOfSteps,
//     camRot.y > qTargetRot.y ? -(camRot.y - qTargetRot.y) / amountOfSteps : (qTargetRot.y - camRot.y) / amountOfSteps,
//     camRot.z > qTargetRot.z ? -(camRot.z - qTargetRot.z) / amountOfSteps : (qTargetRot.z - camRot.z) / amountOfSteps,
//     camRot.w > qTargetRot.w ? -(camRot.w - qTargetRot.w) / amountOfSteps : (qTargetRot.w - camRot.w) / amountOfSteps);
//    (camRot.w - qTargetRot.w) / amountOfSteps);
//Vector3 rotSteps = -((camTransform.rotation.eulerAngles - TargetRot) / amountOfSteps);