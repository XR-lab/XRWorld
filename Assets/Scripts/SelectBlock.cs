using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace XRWorld.SelectBlock
{


    public class SelectBlock : MonoBehaviour
    {
        public bool useMouseInput = true;
        public bool openUI = false;
        private ARRaycastManager _raycastManager;
        [SerializeField] private PlacementBlock[] placedObjects;
        private Vector2 _touchPos = default;
        private Camera _arCamera;

        void Start()
        {
            ChangeSelectedObject(placedObjects[0]);
            _arCamera = Camera.main;
        }

        void Update()
        {
            if (useMouseInput)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    FindBlock(Input.mousePosition);
                }
            }
            else
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    _touchPos = touch.position;

                    if (touch.phase == TouchPhase.Began)
                    {
                        FindBlock(touch.position);
                    }
                }
            }
        }


        void FindBlock(Vector3 inputPosition)
        {
            Ray ray = _arCamera.ScreenPointToRay(inputPosition);
            RaycastHit hitObject;
            if (Physics.Raycast(ray, out hitObject))
            {
                PlacementBlock placementBlock = hitObject.transform.GetComponent<PlacementBlock>();
                if (placementBlock != null)
                {
                    ChangeSelectedObject(placementBlock);
                }
            }
        }

        /*
        void FindBlock()
        
        {
            
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                _touchPos = touch.position;
    
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = _arCamera.ScreenPointToRay(touch.position);
                    RaycastHit hitObject;
                    if (Physics.Raycast(ray, out hitObject))
                    {
                        PlacementBlock placementBlock = hitObject.transform.GetComponent<PlacementBlock>();
                        if (placementBlock != null)
                        {
                            ChangeSelectedObject(placementBlock);
                        }
                    }
                }
            }
        }
        */
        void ChangeSelectedObject(PlacementBlock selected)
        {
            foreach (PlacementBlock current in placedObjects)
            {
                MeshRenderer meshRenderer = current.GetComponent<MeshRenderer>();
                if (selected != current)
                {
                    current.isSelected = false;
                    meshRenderer.material.color = Color.yellow;
                }
                else
                {
                    current.isSelected = true;
                    meshRenderer.material.color = Color.magenta;
                }
            }
        }
    }
}