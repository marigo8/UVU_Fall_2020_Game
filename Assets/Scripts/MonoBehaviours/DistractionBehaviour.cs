using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    
    public Vector3 force;
    public float life;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        var forceDirection = force;
        rb.AddRelativeForce(forceDirection);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            life -= Time.fixedDeltaTime;
            Debug.Log(Time.time);

            if (life <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
