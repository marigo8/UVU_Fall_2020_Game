using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Set the location data of the player to the current spawn point.
        var cm = other.GetComponent<PlayerController>();
        if (cm != null)
        {
            cm.currentSpawnPoint.SetTransform(transform);
        }
        
    }

}
