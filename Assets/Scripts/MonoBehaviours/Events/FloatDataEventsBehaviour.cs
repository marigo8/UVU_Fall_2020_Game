using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloatDataEventsBehaviour : MonoBehaviour
{
    public FloatData data;
    public UnityEvent zeroEvent;
    private bool isZero;

    public void Update()
    {
        if (data.value == 0)
        {
            if (!isZero)
            {
                isZero = true;
                zeroEvent.Invoke();
            }
        }
        else
        {
            isZero = false;
        }
    }
}
