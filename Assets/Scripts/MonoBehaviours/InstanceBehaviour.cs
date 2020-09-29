using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InstanceBehaviour : MonoBehaviour
{
    // private Rigidbody rb;
    // public Vector3 force;
    //
    // private void Start()
    // {
    //     rb = GetComponent<Rigidbody>();
    //     var forceDirection = force;
    //     rb.AddRelativeForce(forceDirection);
    // }

    public void CreateInstance(Transform transformObj)
    {
        Instantiate(gameObject, transformObj.position, transformObj.rotation);
    }
}
