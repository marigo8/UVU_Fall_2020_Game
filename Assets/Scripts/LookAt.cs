using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform lookAtObj;
    private void Update()
    {
        var lookAt = Vector3.zero;
        var rot = transform.eulerAngles;
        rot.x = 0;
        transform.rotation = Quaternion.Euler(rot);
    }
}
