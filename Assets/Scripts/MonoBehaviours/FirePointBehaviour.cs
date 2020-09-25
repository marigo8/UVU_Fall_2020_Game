using System;
using UnityEngine;

public class FirePointBehaviour : MonoBehaviour
{
    public GameObject projectile;
    public Vector3 offset;

    private Vector3 location;
    private Quaternion rotation;
    
    private Camera cam;
    public Vector3Data target;

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
                target.value = hit.point;

                transform.LookAt(target.value);
                var rot = transform.eulerAngles;
                rot.x = 0;
                transform.rotation = Quaternion.Euler(rot);
            }
        }
    }
    
    public void InstantiateProjectile()
    {
        
        rotation = transform.rotation;
        location = transform.position + offset;
        
        var direction = location - transform.position;
        direction = rotation * direction;
        location = direction + transform.position;

        Instantiate(projectile,location,rotation);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + offset, .25f);
    }
}
