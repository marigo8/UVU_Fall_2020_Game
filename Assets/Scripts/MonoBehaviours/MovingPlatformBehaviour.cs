using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingPlatformBehaviour : MonoBehaviour
{
    [HideInInspector] public Vector3 velocity;
    [SerializeField] private Vector3 relativeDestination;
    [SerializeField] private float moveTime, waitTime;
    private bool moving = true, movingForwards = true;
    private float elapsedTime;
    private Vector3 previousPosition, pointA, pointB;

    private void Start()
    {
        pointA = transform.position;
        pointB = transform.position + relativeDestination;
    }
    private void FixedUpdate()
    {
        previousPosition = transform.position;
        
        elapsedTime += Time.fixedDeltaTime;
        if (moving)
        {
            var step = elapsedTime / moveTime;
            
            if (step >= 1)
            {
                moving = false;
                movingForwards = !movingForwards;
                elapsedTime = 0;
                return;
            }

            if (movingForwards)
            {
                transform.position = Vector3.Lerp(pointA, pointB, step);
            }
            else
            {
                transform.position = Vector3.Lerp(pointB, pointA, step);
            }
        }
        else
        {
            if (elapsedTime >= waitTime)
            {
                moving = true;
                elapsedTime = 0;
            }
        }

        velocity = transform.position - previousPosition;
    }

    private void OnCollisionEnter(Collision other)
    {
        print("WAHH!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position + relativeDestination, transform.localScale);
        Gizmos.DrawLine(transform.position, transform.position + relativeDestination);
    }
}
