using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    
    public Vector3 force;
    public bool hasLifeTime;
    public float lifeTime;

    private IEnumerator Start()
    {
        rb = GetComponent<Rigidbody>();
        var forceDirection = force;
        rb.AddRelativeForce(forceDirection);

        if (!hasLifeTime) yield break;
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
