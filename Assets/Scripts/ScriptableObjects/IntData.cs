using UnityEngine;

[CreateAssetMenu]
public class IntData : ScriptableObject
{
    public bool useClamp;
    public int value, startingValue, maxValue, startingMax;

    public void Reset()
    {
        value = startingValue;
    }

    public void ResetMax()
    {
        maxValue = startingMax;
    }

    public void UpdateValue(int amount)
    {
        value += amount;
        if (useClamp)
        {
            ClampValue();
        }
    }

    public void SetValue(int amount)
    {
        value = amount;
        if (useClamp)
        {
            ClampValue();
        }
    }

    public void UpdateMaxValue(int amount)
    {
        maxValue += amount;
        if (useClamp)
        {
            ClampValue();
        }
    }

    public void SetMaxValue(int amount)
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
