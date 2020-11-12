using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class IntData : ScriptableData
{
    public int value, maxValue;
    public bool useClamp;
    public bool IsMaxed => value >= maxValue;

    public UnityEvent updateValueEvent;
    public UnityEvent zeroEvent;

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
        SetValue(maxValue);
    }

    private void ClampValue()
    {
        if (useClamp)
        {
            value = Mathf.Clamp(value, 0, maxValue);

            if (value <= 0)
            {
                zeroEvent.Invoke();
            }
        }
        updateValueEvent.Invoke();
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