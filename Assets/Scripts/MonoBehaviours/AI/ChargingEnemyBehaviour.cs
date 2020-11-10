using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class ChargingEnemyBehaviour : MonoBehaviour
{
    public Transform target;
    public float chargingSpeed = 10f, aimingTime = 5f, chargingTime, minimumSpeed, slowingRate, rotateSpeed;

    public UnityEvent chargeEvent, aimEvent;

    private CharacterController controller;
    private Vector3 movement, gravityForce, lastTargetPosition, aimTarget;
    private bool aiming;

    private readonly WaitForFixedUpdate fixedWait = new WaitForFixedUpdate();
    private WaitForSeconds chargingWait;

    public void StopCharging()
    {
        movement = Vector3.zero;
        StartCoroutine(Aim());
    }

    public void SlowToStop()
    {
        StartCoroutine(SlowToStopCoroutine());
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        chargingWait = new WaitForSeconds(chargingTime);
        
        StartCoroutine(Aim());
    }

    private void Update()
    {
        controller.Move((movement + gravityForce) * Time.deltaTime);
        if (aiming)
        {
            PredictPosition();
        }
    }

    private void FixedUpdate()
    {
        Gravity();
    }
    
    private void Gravity()
    {
        // Gravity
        if (controller.isGrounded)
        {
            gravityForce.y = 0;
        }
        gravityForce.y += Physics.gravity.y * Time.fixedDeltaTime;
    }

    private void PredictPosition() // Inspired by Wopsie's answer at https://answers.unity.com/questions/1125768/how-do-i-predict-the-position-of-my-player-for-the.html
    {
        var targetDistance = Vector3.Distance(transform.position, target.position);
        var travelTime = targetDistance / (chargingSpeed*Time.deltaTime);
        var targetVelocity = (target.position - lastTargetPosition);

        var predictedPosition = target.position + targetVelocity * travelTime;

        lastTargetPosition = target.position;

        aimTarget = predictedPosition;
    }

    private IEnumerator Aim()
    {
        if (aiming) yield break;
        aiming = true;
        aimEvent.Invoke();
        var timeElapsed = 0f;
        while (timeElapsed < aimingTime)
        {
            // transform.LookAt(aimTarget);
            // var rotation = transform.eulerAngles;
            // rotation.x = 0f;
            // transform.eulerAngles = rotation;

            var lookDirection = aimTarget - transform.position;
            var rotation = Quaternion.LookRotation(lookDirection);
            rotation = Quaternion.Lerp(transform.rotation, rotation, rotateSpeed * Time.fixedDeltaTime);
            rotation.x = 0f;
            rotation.z = 0f;
            transform.rotation = rotation;

            yield return fixedWait;
            timeElapsed += Time.fixedDeltaTime;
        }

        aiming = false;
        Charge();
    }

    private void Charge()
    {
        chargeEvent.Invoke();
        movement = transform.forward * chargingSpeed;
    }

    private IEnumerator SlowToStopCoroutine()
    {
        while (movement.magnitude > minimumSpeed)
        {
            movement = Vector3.Lerp(movement, Vector3.zero, Time.fixedDeltaTime*slowingRate);
            yield return fixedWait;
        }
        movement = Vector3.zero;
        StartCoroutine(Aim());
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if ((controller.collisionFlags & CollisionFlags.Sides) == 0) return;
        Debug.Log("Ouch");
        StopCharging();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, aimTarget);
    }
}
