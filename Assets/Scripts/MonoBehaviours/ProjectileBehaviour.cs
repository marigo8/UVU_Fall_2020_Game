using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    
    [SerializeField] private Vector3 force;
    [SerializeField] private float lifeTime;

    private IEnumerator Start()
    {
        rb = GetComponent<Rigidbody>();
        var forceDirection = force;
        rb.AddRelativeForce(forceDirection);

        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
