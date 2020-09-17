using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;
    private void Update()
    {
        transform.LookAt(target);
        var rot = transform.eulerAngles;
        rot.x = 0;
        transform.rotation = Quaternion.Euler(rot);
    }
}
