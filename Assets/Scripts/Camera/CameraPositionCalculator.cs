using System.Collections;
using UnityEngine;
using XRWorld.Database;

namespace XRWorld.Core
{
    public class CameraPositionCalculator : MonoBehaviour
    {
        // Update logic
        private bool _active = false, _camIntervalActive = false;

        // for calculating where the middle of the level is
        [SerializeField] private Transform _levelSpawn;
        [SerializeField] private Vector3 _offsetToTheMiddle;
        private Vector3 _theMiddle;

        private CameraChangeHandler _camHandler;
        void Start()
        {
            _camHandler = FindObjectOfType<CameraChangeHandler>();
        }

        public void Setup()
        {
            _theMiddle = _levelSpawn.position + _offsetToTheMiddle;
            _camHandler.AddCameraDataToDatabase(transform.position - _theMiddle, transform.rotation.eulerAngles);

            _active = true;
        }

        void Update()
        {
            if(!_active || _camIntervalActive) return;
            StartCoroutine(CamInterval());
        }

        IEnumerator CamInterval()
        {
            _camIntervalActive = true;
            
            yield return new WaitForSeconds(0.5f);
            
            Vector3 cameraOffsetToCenter = transform.position - _theMiddle;
            _camHandler.ParseUpdatedCameraData(cameraOffsetToCenter, transform.rotation.eulerAngles);
            
            _camIntervalActive = false;
        }
    }
}
