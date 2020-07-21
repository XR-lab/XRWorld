
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using XRWorld.Core;
using XRWorld.Database;

namespace XRWorld.ArTapToPlace
{

    public class ArTapToPlaceObject : MonoBehaviour
    {
        public GameObject placementIndicator;
        
        private JSONParser _jsonParser;
        private LevelSpawner _levelSpawner;
        private ARRaycastManager _raycastManager;
        private Pose _placementPose;
        private bool _placementPoseIsValid = false;
        public bool isPlaced = false;

        void Start()
        {
            _jsonParser = FindObjectOfType<JSONParser>();
            _levelSpawner = FindObjectOfType<LevelSpawner>();
            _raycastManager = FindObjectOfType<ARRaycastManager>();
            
        }

        void Update()
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();

            if (Input.touchCount > 0 && isPlaced == false )
            {
                PlaceObject();
            }

        }

        private void PlaceObject()
        {
            LevelData data = _jsonParser.ParseJSONToLevelData();
            _levelSpawner.SpawnLevel(data, _placementPose.position, _placementPose.rotation);
            isPlaced = true;
            //Instantiate(objectToPlace, _placementPose.position, _placementPose.rotation);
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
                Debug.Log(hits[0]);
                _placementPose = hits[0].pose;

                var cameraForward = Camera.main.transform.forward;
                var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
                _placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            }
        }
    }
}