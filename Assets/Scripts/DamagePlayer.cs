using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Collider))]
public class DamagePlayer : MonoBehaviour
{
    public IntData damage;
    private void OnTriggerEnter(Collider other)
    {
        var cm = other.GetComponent<CharacterMover>();
        if (cm != null)
        {
            cm.TakeDamage(damage.value);
        }
        
    }
}
