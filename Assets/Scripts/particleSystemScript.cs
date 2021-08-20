using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleSystemScript : MonoBehaviour
{
    private ParticleSystem ps;

    private void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        ps.Play();
    }
    
}
