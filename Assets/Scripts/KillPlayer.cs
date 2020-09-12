using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        var cm = other.GetComponent<CharacterMover>();
        if (cm != null)
        {
            cm.playerHealth.value = 0;
        }
        
    }
}
