using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class FloatData : ScriptableObject
{
    public string label;
    public float value;
    public float maxValue;
    public bool useClamp;

    public bool IsMaxed => value >= maxValue;

    public void AddToValue(float amount)
    {
        value += amount;
        ClampValue();
    }

    public void SetValue(float amount)
    {
        value = amount;
        ClampValue();
    }

    public void AddToMaxValue(float amount)
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

    private void ClampValue()
    {
        if (!useClamp) return;

        if (value < 0f)
        {
            value = 0f;
        }
        else if (value > maxValue)
        {
            SetValueToMax();
        }
    }
}
