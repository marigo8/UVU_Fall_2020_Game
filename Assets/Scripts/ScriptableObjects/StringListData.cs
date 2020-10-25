using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StringListData : ScriptableData
{
    public List<string> data;
    public int index;

    public string GetNextString()
    {
        return data[index];
    }
}
