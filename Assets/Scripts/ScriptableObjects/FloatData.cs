using UnityEngine;

[CreateAssetMenu]
public class FloatData : ScriptableObject
{
    public bool hasMax;
    public float value, maxValue;

    public void UpdateValue(float amount)
    {
        value += amount;
        if (hasMax)
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
    }
}
