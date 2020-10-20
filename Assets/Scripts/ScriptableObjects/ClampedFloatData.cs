using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ClampedFloatData : FloatData
{
    public float maxValue, startingMax;

    public bool IsMaxed => value >= maxValue;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        if (useStartingValue)
        {
            maxValue = startingMax;
        }
    }

    public override void AddToValue(float amount)
    {
        value += amount;
        ClampValue();
    }

    public override void SetValue(float amount)
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
        value = Mathf.Clamp(value, 0, maxValue);
    }

    public override string GetString()
    {
        var text = label + ": " + value.ToString("F1");
        
        text += " / " + maxValue.ToString("F1");

        return text;
    }
}
