using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyBehaviour : MonoBehaviour
{
    // [SerializeField] private IntData damage;
    // [SerializeField] private float knockBackForce;
    // private void OnTriggerStay(Collider other)
    // {
    //     var cm = other.GetComponent<PlayerBehaviour>();
    //     if (cm == null) return;
    //     
    //     var force = other.transform.position - transform.position;
    //     force = force.normalized * knockBackForce;
    //     cm.TakeDamage(damage.value, force);
    //
    // }
}
