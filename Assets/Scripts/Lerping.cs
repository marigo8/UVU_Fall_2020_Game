using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerping : MonoBehaviour
{
    public Vector3 vOne, vTwo;
    public float speed = 2f;
    
    private void Update()
    {
        var direction = Vector3.Lerp(vOne, vTwo, speed * Time.deltaTime);
        
    }
}
