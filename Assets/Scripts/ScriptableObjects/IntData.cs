using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class IntData : ScriptableData
{
    public int value, maxValue, startValue, startMax;
    public bool useClamp;
    public bool IsMaxed => value >= maxValue;

    public UnityEvent zeroEvent;

    private void OnEnable()
    {
        if (!useStartingValue) return;
        value = startValue;
        maxValue = startMax;
    }


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

        if (value <= 0)
        {
            zeroEvent.Invoke();
        }
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