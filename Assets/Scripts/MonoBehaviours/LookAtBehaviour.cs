using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3Data target;
    [SerializeField] private bool lockX, lockY;

    public void LookAtTarget()
    {
        transform.LookAt(target.value);
        
        var eulerRotation = transform.eulerAngles;
        if (lockX)
        {
            eulerRotation.x = 0f;
        }

        if (lockY)
        {
            eulerRotation.y = 0f;
        }
        
        transform.eulerAngles = eulerRotation;
    }
}
