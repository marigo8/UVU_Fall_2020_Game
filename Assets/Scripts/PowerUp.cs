using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PowerUp : MonoBehaviour
{
    public float respawnTime;
    public UnityEvent powerUpAction;

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() == null) yield break;
        
        powerUpAction.Invoke();

        if (respawnTime > 0f)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            
            yield return new WaitForSeconds(respawnTime);
            
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
