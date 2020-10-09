using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FloatDataHudBehaviour : MonoBehaviour
{
    public FloatData data;
    public int decimalPlaces;
    private Text textObj;

    private void Start()
    {
        textObj = GetComponent<Text>();
    }

    private void Update()
    {
        var text = data.label + ": " + data.value.ToString("F"+decimalPlaces);
        if (data.useClamp)
        {
            text += " / " + data.maxValue.ToString("F"+decimalPlaces);
        }
        
        textObj.text = text;
    }
}
