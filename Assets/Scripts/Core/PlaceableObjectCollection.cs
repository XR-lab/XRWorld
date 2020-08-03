using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XRWorld.Core
{
    [CreateAssetMenu(fileName = "PlaceableObjectCollection", menuName = "XR-World/PlaceableObjectCollection", order = 1)]
    public class PlaceableObjectCollection : ScriptableObject
    {
        // TODO: convert to class with implements upgrade and downgrade costs.
        [SerializeField] private GameObject[] _placeableObjectStates;

        public GameObject GetGameObjectByLevel(int level)
        {
            return _placeableObjectStates[level];
        }
        
    }
}