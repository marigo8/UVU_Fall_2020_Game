using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StringListData : ScriptableData
{
    public List<string> data;
    private string returnString;
    public int index;

    public void GetNextString()
    {
        returnString = data[index];
    }
}
