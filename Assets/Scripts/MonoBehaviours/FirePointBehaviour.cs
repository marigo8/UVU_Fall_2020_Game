using System;
using UnityEngine;

public class FirePointBehaviour : MonoBehaviour
{
    public GameObject prefab;
    public Vector3 offset;

    private Vector3 location;
    private Quaternion rotation;
    
    private Camera cam;
    public Transform target;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        //  v Use this to detect mouse input instead
        if (Input.GetMouseButtonDown(0))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                target.position = hit.point;

                transform.LookAt(target);
                var rot = transform.eulerAngles;
                rot.x = 0;
                transform.rotation = Quaternion.Euler(rot);
                
                InstantiatePrefab();
            }
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
