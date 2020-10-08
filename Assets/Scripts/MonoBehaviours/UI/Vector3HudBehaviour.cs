﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vector3HudBehaviour : MonoBehaviour
{
    public Vector3Data data;
    private Text textObj;

    private void Start()
    {
        textObj = GetComponent<Text>();
    }

    private void Update()
    {
        var text = data.label + ": " + data.value;
        
        textObj.text = text;
    }
    
}
