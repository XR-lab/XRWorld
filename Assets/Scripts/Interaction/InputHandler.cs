using UnityEngine;
using XRWorld.Core;
namespace XRWorld.Interaction
{
    [RequireComponent(typeof(TileSelector))]
    public class InputHandler : MonoBehaviour
    {
        public bool useMouseInput = true;
        [SerializeField] private LayerMask _mask;

        private Vector2 _touchPos;
        private Camera _arCamera;
        private TileSelector _tileSelector;

        private void Awake()
        {
            _tileSelector = GetComponent<TileSelector>();
        }

        void Start()
        {
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
                        FindBlock(_touchPos);
                    }
                }
            }
        }


        void FindBlock(Vector3 inputPosition)
        {
            Ray ray = _arCamera.ScreenPointToRay(inputPosition);
            RaycastHit hitObject;
            if (Physics.Raycast(ray, out hitObject, Mathf.Infinity, _mask))
            {
                print(hitObject.transform.name);
                Tile tile = hitObject.transform.GetComponent<Tile>();
                if (tile != null)
                {
                    _tileSelector.SelectTile(tile);
                }
            }
        }
        
        /*
        void ChangeSelectedObject(Tile selected)
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
        */
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
        
    }
}