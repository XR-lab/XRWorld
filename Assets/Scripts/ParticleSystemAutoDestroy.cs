using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemAutoDestroy : MonoBehaviour
{
    [SerializeField] private ParticleSystem _ps;

    private void Start()
    {
        Destroy(gameObject, _ps.main.duration);
    }
}
