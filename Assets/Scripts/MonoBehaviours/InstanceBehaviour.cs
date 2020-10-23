using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceBehaviour : MonoBehaviour
{
    public IntData count;
    public void CreateInstance(Transform transformObj)
    {
        if (count != null)
        {
            if (count.value <= 0) return;
            count.AddToValue(-1);
        }
        Instantiate(gameObject, transformObj.position, transformObj.rotation);
    }
}
