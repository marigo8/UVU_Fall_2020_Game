using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LerpBehaviour : MonoBehaviour
{
    public Vector3 relativeDestination;
    public float moveTime, waitTime;
    private bool moving = true, movingForwards = true;
    private float elapsedTime;
    private Vector3 pointA, pointB;

    private void Start()
    {
        pointA = transform.position;
        pointB = transform.position + relativeDestination;
    }
    private void FixedUpdate()
    {
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
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position + relativeDestination, transform.localScale);
        Gizmos.DrawLine(transform.position, transform.position + relativeDestination);
    }

    public void SetColliderParent(Collider other)
    {
        other.transform.parent = transform;
    }

    public void ClearColliderParent(Collider other)
    {
        other.transform.parent = null;
    }
}