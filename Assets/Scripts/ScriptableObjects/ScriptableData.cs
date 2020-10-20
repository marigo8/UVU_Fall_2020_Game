using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableData : ScriptableObject
{
    public string label;
    public bool useStartingValue;
    
    public virtual string GetString()
    {
        return "not implemented";
    }
}
