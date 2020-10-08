using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class FloatData : ScriptableObject
{
    public string label;
    public bool useClamp;
    public float value, maxValue;

    public void UpdateValue(float amount)
    {
        value += amount;
        ClampValue();
    }

    public void SetValue(float amount)
    {
        value = amount;
        ClampValue();
    }

    public void UpdateMaxValue(float amount)
    {
        maxValue += amount;
        ClampValue();
    }

    public void SetMaxValue(float amount)
    {
        maxValue = amount;
        ClampValue();
    }

    public void SetValueToMax()
    {
        value = maxValue;
    }

    public void SetValueToZero()
    {
        value = 0;
    }

    public float GetFraction()
    {
        return value / maxValue;
    }

    private void ClampValue()
    {
        if (!useClamp) return;
        if (value > maxValue)
        {
            SetValueToMax();
        }
        else if (value < 0)
        {
            SetValueToZero();
        }
    }

    private void SetFromVectorX(Vector3Data vector)
    {
        value = vector.value.x;
    }
    
    private void SetFromVectorY(Vector3Data vector)
    {
        value = vector.value.y;
    }
    
    private void SetFromVectorZ(Vector3Data vector)
    {
        value = vector.value.z;
    }
}
