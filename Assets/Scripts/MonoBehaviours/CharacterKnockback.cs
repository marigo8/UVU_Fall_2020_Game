using System;
using System.Collections;
using UnityEngine;

public class CharacterKnockback : MonoBehaviour
{
    public CharacterController controller;
    public Vector3 knockBackVector;
    public float knockBackForce = 50f;
    private float tempForce;

    private void Start()
    {
        tempForce = knockBackForce;
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        knockBackForce = tempForce;
        while (knockBackForce > 0)
        {
            knockBackVector.x = knockBackForce*Time.fixedDeltaTime;
            controller.Move(knockBackVector);
            knockBackForce -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}
