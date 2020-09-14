using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Vector3Data vData;
    
    // Set the vData from the position value on start
    private void Start()
    {
        vData.SetValue(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Set the location data of the player to the current spawn point.
        var cm = other.GetComponent<CharacterMover>();
        if (cm != null)
        {
            cm.currentSpawnPoint = vData;
        }
        
    }

}
