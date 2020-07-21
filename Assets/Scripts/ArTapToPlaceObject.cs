
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace XRWorld.ArTapToPlace
{


    public class ArTapToPlaceObject : MonoBehaviour
    {
        public GameObject objectToPlace;
        public GameObject placementIndicator;

        private ARRaycastManager _raycastManager;
        private Pose _placementPose;
        private bool _placementPoseIsValid = false;
        public bool isPlaced = false;

        void Start()
        {
            _raycastManager = FindObjectOfType<ARRaycastManager>();

        }

        void Update()
        {
            UpdatePlacementPose();
            UpdatePlacementIndecator();

            if (Input.touchCount > 0 && isPlaced == false)
            {
                PlaceObject();
            }

        }

        private void PlaceObject()
        {
            isPlaced = true;
            Instantiate(objectToPlace, _placementPose.position, _placementPose.rotation);
        }

        private void UpdatePlacementIndecator()
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
            Debug.Log(hits[0]);
            _placementPoseIsValid = hits.Count > 0;
            if (_placementPoseIsValid)
            {
                _placementPose = hits[0].pose;

                var cameraForward = Camera.main.transform.forward;
                var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
                _placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            }
        }
    }
}