using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class FloatData : ScriptableData
{
    public float value, startingValue;

    protected virtual void OnEnable()
    {
        if (useStartingValue)
        {
            value = startingValue;
        }
    }
    
    public virtual void AddToValue(float amount)
    {
        value += amount;
    }

    public virtual void SetValue(float amount)
    {
        value = amount;
    }

    public override string GetString()
    {
        var text = label + ": " + value.ToString("F1");

        return text;
    }

    public void SetValueFromRotationY(Transform transformObj)
    {
        value = transformObj.eulerAngles.y;
    }

    public void SetRotationYFromValue(Transform transformObj)
    {
        var rotation = transformObj.eulerAngles;
        rotation.y = value;
        transformObj.eulerAngles = rotation;
    }
}
