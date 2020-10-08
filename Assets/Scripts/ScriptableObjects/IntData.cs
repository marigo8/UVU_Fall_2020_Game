using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class IntData : ScriptableObject
{
    public string label;
    public bool useClamp;
    public int value, maxValue;

    public void UpdateValue(int amount)
    {
        value += amount;
        ClampValue();
    }

    public void SetValue(int amount)
    {
        value = amount;
        ClampValue();
    }

    public void UpdateMaxValue(int amount)
    {
        maxValue += amount;
        ClampValue();
    }

    public void SetMaxValue(int amount)
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
}