using UnityEngine;

[CreateAssetMenu]
public class FloatData : ScriptableObject
{
    public bool useClamp;
    public float value, startingValue, maxValue, startingMax;

    public void Reset()
    {
        value = startingValue;
    }

    public void ResetMax()
    {
        maxValue = startingMax;
    }

    public void UpdateValue(float amount)
    {
        value += amount;
        if (useClamp)
        {
            ClampValue();
        }
    }

    public void SetValue(float amount)
    {
        value = amount;
        if (useClamp)
        {
            ClampValue();
        }
    }

    public void UpdateMaxValue(float amount)
    {
        maxValue += amount;
        if (useClamp)
        {
            ClampValue();
        }
    }

    public void SetMaxValue(float amount)
    {
        maxValue = amount;
        if (useClamp)
        {
            ClampValue();
        }
    }

    public void SetMax()
    {
        value = maxValue;
    }

    public void SetZero()
    {
        value = 0;
    }

    public float GetFraction()
    {
        return value / maxValue;
    }

    private void ClampValue()
    {
        if (value > maxValue)
        {
            SetMax();
        }
        else if (value < 0)
        {
            SetZero();
        }
    }
}
