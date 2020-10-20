using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class IntData : ScriptableData
{
    public int value, startingValue;

    protected virtual void OnEnable()
    {
        if (useStartingValue)
        {
            value = startingValue;
        }
    }

    public virtual void AddToValue(int amount)
    {
        value += amount;
    }

    public virtual void SetValue(int amount)
    {
        value = amount;
    }

    public override string GetString()
    {
        var text = label + ": " + value;

        return text;
    }
}