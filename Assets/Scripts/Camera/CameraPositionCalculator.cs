using UnityEngine;
using XRWorld.Database;

namespace XRWorld.Core
{
    public class CameraPositionCalculator : MonoBehaviour
    {
        // whether or not Update should be active
        private bool _active = false;
        
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
            _camHandler.AddCameraDataToDatabase();

            _active = true;
        }

        void Update()
        {
            if(!_active) return;

            Vector3 cameraOffsetToCenter = transform.position - _theMiddle;
        }
    }

    public class JSONcamData()
    {
        public Vector3 position;

        JSONcamData()
        {
            
        }
    }
}