using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatDataHudBehaviour : MonoBehaviour
{
    public FloatData data;
    private Text textObj;

    private void Start()
    {
        textObj = GetComponent<Text>();
    }

    private void Update()
    {
        var text = data.label + ": " + data.value;
        if (data.useClamp)
        {
            text += " / " + data.maxValue;
        }
        
        textObj.text = text;
    }
}
