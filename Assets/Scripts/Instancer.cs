using System;
using UnityEngine;

public class Instancer : MonoBehaviour
{
    public GameObject prefab;
    public Vector3 offset;

    private Vector3 location;
    private Quaternion rotation;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            InstantiatePrefab();
        }
    }
    
    private void InstantiatePrefab()
    {
        
        rotation = transform.rotation;
        location = transform.position + offset;
        
        var direction = location - transform.position;
        direction = rotation * direction;
        location = direction + transform.position;
        
        Instantiate(prefab,location,rotation);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + offset, .25f);
    }
}
