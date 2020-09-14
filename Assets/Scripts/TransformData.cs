using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TransformData : ScriptableObject
{
    public Vector3 position;
    public Vector3 eulerAngles;
    public Vector3 scale = Vector3.one;
    
    public Quaternion GetRotation()
    {
        return Quaternion.Euler(eulerAngles);
    }

    public void SetTransform(Transform newTransform)
    {
        position = newTransform.position;
        eulerAngles = newTransform.rotation.eulerAngles;
        scale = newTransform.localScale;
    }
}
