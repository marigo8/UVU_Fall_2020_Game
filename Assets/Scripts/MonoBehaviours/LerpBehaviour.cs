using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(Rigidbody))]
public class LerpBehaviour : MonoBehaviour
{
    public Transform pointBTransform;
    public float moveTime, loopWaitTime;
    public bool loopOnStart;

    public UnityEvent pointAEvent, pointBEvent;

    private bool goToPointA, moving;
    private Vector3 pointA, pointB, currentPosition;

    private Coroutine currentCoroutine;

    private readonly WaitForFixedUpdate fixedWait = new WaitForFixedUpdate();
    private WaitForSeconds loopWait;

    public void Move(bool loop)
    {
        if (moving) return;
        currentCoroutine = StartCoroutine(MoveCoroutine(loop));
    }

    public void MoveToA(bool loop)
    {
        goToPointA = true;
        Move(loop);
    }

    public void MoveToB(bool loop)
    {
        goToPointA = false;
        Move(loop);
    }

    public void Stop()
    {
        if (!moving) return;
        StopCoroutine(currentCoroutine);
        moving = false;
    }

    public void SetColliderParent(Collider other)
    {
        other.transform.parent = transform;
    }

    public void ClearColliderParent(Collider other)
    {
        other.transform.parent = null;
    }

    private void Start()
    {
        pointA = transform.position;
        pointB = pointBTransform.position;
        currentPosition = transform.position;
        
        loopWait = new WaitForSeconds(loopWaitTime);
        
        if (loopOnStart)
        {
            Move(true);
        }
    }

    private void FixedUpdate()
    {
        transform.position = currentPosition;
        // For some reason, the player doesn't move with the platform unless I set the transform in a FixedUpdate like this. No idea why. ¯\_(:/)_/¯
    }

    private IEnumerator MoveCoroutine(bool loop)
    {
        moving = true;
        var startingPoint = pointA;
        var endingPoint = pointB;
        if (goToPointA)
        {
            startingPoint = pointB;
            endingPoint = pointA;
        }

        var abDistance = Vector3.Distance(startingPoint, endingPoint);
        var currentDistance = Vector3.Distance(transform.position, startingPoint);
        var progress = currentDistance / abDistance;
        progress *= moveTime;

        for (var i = progress; i < moveTime; i += Time.fixedDeltaTime)
        {
            currentPosition = Vector3.Lerp(startingPoint, endingPoint, i / moveTime);
            yield return fixedWait;
        }

        currentPosition = endingPoint; // for precision
        if (goToPointA)
        {
            pointAEvent.Invoke();
        }
        else
        {
            pointBEvent.Invoke();
        }

        goToPointA = !goToPointA;
        moving = false;

        if (!loop) yield break;
        yield return loopWait;
        Move(true);
    }

    private void OnDrawGizmos()
    {
        if (pointBTransform == null) return;
        Gizmos.DrawWireCube(pointBTransform.position, transform.localScale);
        Gizmos.DrawLine(transform.position, pointBTransform.position);
    }
}