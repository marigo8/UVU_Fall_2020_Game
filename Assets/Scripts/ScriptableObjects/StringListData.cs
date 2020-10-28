using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StringListData : ScriptableObject
{
    public int index;
    public List<string> data;

    private void OnEnable()
    {
        index = 0;
    }

    public string GetNextString()
    {
        var line = data[index];
        index = (index + 1) % data.Count;
        return line;
    }
}
