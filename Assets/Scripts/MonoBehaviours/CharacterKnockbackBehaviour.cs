using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterKnockbackBehaviour : MonoBehaviour
{
    public float knockbackForce;
    private void OnTriggerEnter(Collider other)
    {
        var character = other.GetComponent<CharacterMover>();
        if (character == null) return;
        
        var direction = other.transform.position - transform.position;
        direction.Normalize();
        
        character.AddForce(direction * knockbackForce);
    }
}
