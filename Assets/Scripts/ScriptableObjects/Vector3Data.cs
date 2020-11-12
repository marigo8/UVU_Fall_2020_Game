using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class Vector3Data : ScriptableData
{
    public Vector3 value;
    public UnityEvent updateValueEvent;

    public float x => value.x;
    public float y => value.y;
    public float z => value.z;

    public void SetValueFromVector3(Vector3 vector3)
    {
        value = vector3;
        updateValueEvent.Invoke();
    }

    public void SetValueFromPosition(Transform transformObj)
    {
        value = transformObj.position;
        updateValueEvent.Invoke();
    }

    public void SetValueFromRotation(Transform transformObj)
    {
        value = transformObj.eulerAngles;
        updateValueEvent.Invoke();
    }

    public void SetPositionFromValue(Transform transformObj)
    {
        transformObj.position = value;
    }

    public void SetRotationFromValue(Transform transformObj)
    {
        transformObj.eulerAngles = value;
    }

    public void SetFromMousePosition()
    {
        if (Camera.main == null) return;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit);
        value = hit.point;
        updateValueEvent.Invoke();
    }

    public override string GetString()
    {
        var text = label + ": " + value;
        return text;
    }
}