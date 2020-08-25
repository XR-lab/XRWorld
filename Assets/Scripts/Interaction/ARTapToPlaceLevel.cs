using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using XRWorld.Core;
using XRWorld.Database;

namespace XRWorld.Interaction
{
    public class ARTapToPlaceLevel : MonoBehaviour
    {
        // for debugging purpose so we can easily run in editor
        [SerializeField] private bool _runInEditor;
        
        public GameObject placementIndicator;
        
        private LevelSpawner _levelSpawner;
        private ARRaycastManager _raycastManager;
        private Pose _placementPose;
        private bool _placementPoseIsValid = false;
        public bool isPlaced = false;
        private ARPlaneManager _arPlaneManager;
        private LevelData _levelData;

        void Start()
        {
            _levelSpawner = FindObjectOfType<LevelSpawner>();
            _raycastManager = FindObjectOfType<ARRaycastManager>();
            _arPlaneManager = FindObjectOfType<ARPlaneManager>();
        }

        void Update()
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();

            if (Input.touchCount > 0 && isPlaced == false && _placementPoseIsValid)
            {
                PlaceLevel();
               // this.gameObject.SetActive(false);
            }

        }

        private void PlaceLevel()
        {
            Vector3 spawnPosition = _placementPose.position;
            
            // place object, in ok position when we test in editor...
            if (_runInEditor)
                spawnPosition = new Vector3(-2, -3, 6);
                
            _levelSpawner.SpawnLevel(_levelData, spawnPosition, _placementPose.rotation);
            isPlaced = true;
            placementIndicator.SetActive(false);
            enabled = false;
            DisablePlaneDetection();
            
        }

        private void DisablePlaneDetection()
        {
            _arPlaneManager.enabled = !_arPlaneManager.enabled;

            foreach (ARPlane plane in _arPlaneManager.trackables)
            {
             plane.gameObject.SetActive(_arPlaneManager.enabled = false);   
            }
        }

        private void UpdatePlacementIndicator()
        {
            if (_placementPoseIsValid)
            {
                placementIndicator.SetActive(true);
                placementIndicator.transform.SetPositionAndRotation(_placementPose.position, _placementPose.rotation);
            }
            else
            {
                placementIndicator.SetActive(false);
            }
        }

        private void UpdatePlacementPose()
        {
            Vector2 screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            var hits = new List<ARRaycastHit>();
            _raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);
          
            _placementPoseIsValid = hits.Count > 0;
            if (_placementPoseIsValid)
            {
                _placementPose = hits[0].pose;

                var cameraForward = Camera.main.transform.forward;
                var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
                _placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            }
        }
        
        // Invoked via unity event
        public void SetLevelData(LevelData data)
        {
            _levelData = data;
            if (_runInEditor)
                PlaceLevel();
        }
    }
}