using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransformBehaviour : MonoBehaviour
{
    public Transform target;
    public bool copyPosition = true, copyRotation, copyScale;

    private void Update()
    {
        if (copyPosition)
        {
            transform.position = target.position;
        }

        if (copyRotation)
        {
            transform.rotation = target.rotation;
        }

        if (copyScale)
        {
            transform.localScale = target.localScale;
        }
    }
}
