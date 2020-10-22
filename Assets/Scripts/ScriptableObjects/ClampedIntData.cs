using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ClampedIntData : IntData
{
    public int maxValue, startingMax;

    public bool IsMaxed => value >= maxValue;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (useStartingValue)
        {
            maxValue = startingMax;
        }
    }

    public override void AddToValue(int amount)
    {
        value += amount;
        ClampValue();
    }

    public override void SetValue(int amount)
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
        value = Mathf.Clamp(value, 0, maxValue);
    }

    public override string GetString()
    {
        var text = label + ": " + value;
        
        text += " / " + maxValue;

        return text;
    }

    public override float GetFraction()
    {
        var fraction = (value * 1f) / maxValue;

        return fraction;
    }
}
