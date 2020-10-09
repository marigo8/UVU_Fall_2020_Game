using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
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
        textObj.text = data.GetString();
    }
}
