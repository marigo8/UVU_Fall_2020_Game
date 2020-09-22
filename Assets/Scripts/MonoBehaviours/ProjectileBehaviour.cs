using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 force;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        var forceDirection = force;
        rb.AddRelativeForce(forceDirection);
    }
}
