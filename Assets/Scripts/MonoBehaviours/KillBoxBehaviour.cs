using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoxBehaviour : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        var cm = other.GetComponent<PlayerBehaviour>();
        if (cm != null)
        {
            cm.playerHealth.value = 0;
        }
        
    }
}
