using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Collider))]
public class DamagePlayer : MonoBehaviour
{
    [SerializeField] private IntData damage;
    [SerializeField] private float knockBackForce;
    private void OnTriggerStay(Collider other)
    {
        var cm = other.GetComponent<CharacterMover>();
        if (cm == null) return;
        
        var force = other.transform.position - transform.position;
        force = force.normalized * knockBackForce;
        cm.TakeDamage(damage.value, force);

    }
}
