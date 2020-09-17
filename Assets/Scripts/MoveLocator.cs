using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLocator : MonoBehaviour
{
    private Camera cam;
    public Transform pointObj;

    private void Start()
    {
        cam = Camera.main;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                pointObj.position = hit.point;
            }
        }
    }
}
