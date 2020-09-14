using System;
using UnityEngine;

public class Instancer : MonoBehaviour
{
    public GameObject prefab;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            InstantiatePrefab();
        }
    }
    
    private void InstantiatePrefab()
    {
        var location = transform.position;
        var rotation = transform.rotation;
        Instantiate(prefab,location,rotation);
    }
}
