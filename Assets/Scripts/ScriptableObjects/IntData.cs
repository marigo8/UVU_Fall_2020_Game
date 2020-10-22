using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntData : ScriptableData
{
    public int value, maxValue, startValue, startMax;
    public bool useClamp;

    private void OnEnable()
    {
        if (!useStartingValue) return;
        value = startValue;
        maxValue = startMax;
    }

    public bool IsMaxed => value >= maxValue;

    public void AddToValue(int amount)
    {
        value += amount;
        ClampValue();
    }

    public void SetValue(int amount)
    {
        value = amount;
        ClampValue();
    }

    public void AddToMaxValue(int amount)
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

    private void ClampValue()
    {
        if (!useClamp) return;

        value = Mathf.Clamp(value, 0, maxValue);
    }

    public override string GetString()
    {
        var text = label + ": " + value;
        if (useClamp)
        {
            text += " / " + maxValue;
        }

        return text;
    }

    public override float GetFraction()
    {
        var fraction = (value * 1f) / maxValue;

        return fraction;
    }
}