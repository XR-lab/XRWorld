using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardFX : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;

    private Quaternion _originalRotation;

    void Start()
    {
        _originalRotation = transform.rotation;
    }

    private void Update()
    {
        transform.rotation = _cameraTransform.rotation * _originalRotation;
    }
}