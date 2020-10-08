using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerPushBehaviour : MonoBehaviour
{
    private CharacterController controller;
    public float pushPower;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var rb = hit.rigidbody;
        if (rb == null || rb.isKinematic) return;
        if (hit.moveDirection.y < -0.3f) return;

        var pushDirection = hit.moveDirection;
        pushDirection.y = 0;
        var force = pushDirection * pushPower;

        rb.velocity = force;

    }
}
