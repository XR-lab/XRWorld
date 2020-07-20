﻿  
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ARTapScaleAndSpawn : MonoBehaviour
{
    [SerializeField]private GameObject _placable;
    private GameObject _placedObject;
    private ARRaycastManager _raycastManager;
    private Pose _placementPose;
    private bool _placementPoseIsValid = false;
    private bool _placed = false;
    private ARRaycastHit _arRaycastHit;

    void Start()
    {
        _raycastManager = FindObjectOfType<ARRaycastManager>();
        Debug.Log(_raycastManager);
    }

    void Update()
    {
        UpdatePlacementPose();

        if (_placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetKeyDown(KeyCode.A))
        {
            if (!_placed)
                PlaceObject();
           
        }
    }

    private void SwitchPlane()
    {
        _placedObject.transform.position = _placementPose.position;
        _placedObject.transform.rotation = _placementPose.rotation;
    }

    private void PlaceObject()
    {
        _placedObject = Instantiate(_placable, _placementPose.position, _placementPose.rotation);
        _placed = true;
    }

    private void UpdatePlacementPose()
    {
        Vector2 screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        _raycastManager.Raycast(screenCenter, hits,TrackableType.Planes);
        _placementPoseIsValid = hits.Count > 0;
        if (_placementPoseIsValid)
        {
            _arRaycastHit = hits[0];
            _placementPose = _arRaycastHit.pose;

            var cameraForward = Camera.main.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x,0, cameraForward.z).normalized;
            _placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
    
}