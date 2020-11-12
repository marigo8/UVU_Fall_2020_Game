using UnityEngine;
using UnityEngine.Events;

public class ScriptableData : ScriptableObject
{
    public string label;
    
    public virtual string GetString()
    {
        return "not implemented";
    }

    public virtual float GetFraction()
    {
        return 1f;
    }
}
