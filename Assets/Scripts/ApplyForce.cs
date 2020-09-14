using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ApplyForce : MonoBehaviour
{
    private Rigidbody rb;
    public float force = 300f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        var forceDirection = new Vector3(0, 0, force);
        rb.AddRelativeForce(forceDirection);
    }
}
