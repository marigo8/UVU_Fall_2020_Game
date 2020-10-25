using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AddForceBehaviour : MonoBehaviour{

    public Vector3 force;
    
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        var forceDirection = force;
        rb.AddRelativeForce(forceDirection);
    }
    
}
