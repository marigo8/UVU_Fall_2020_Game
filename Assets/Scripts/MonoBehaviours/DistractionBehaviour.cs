using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionBehaviour : MonoBehaviour
{
    public IntData count;
    public float life;
    


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("EnemyPickUp"))
        {
            life -= Time.fixedDeltaTime;
            Debug.Log(life);

            if (life <= 0)
            {
                Destroy(transform.parent.gameObject);
            }
        }
        else if (other.CompareTag("Player"))
        {
            count.AddToValue(1);
            Destroy(transform.parent.gameObject);
        }
    }
}
