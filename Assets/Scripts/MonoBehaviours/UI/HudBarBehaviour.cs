using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HudBarBehaviour : MonoBehaviour
{
    public ScriptableData data;
    public Gradient gradient;
    private Image imageObj;

    private void Start()
    {
        imageObj = GetComponent<Image>();
    }

    private void Update()
    {
        var fraction = data.GetFraction();

        imageObj.fillAmount = fraction;
        imageObj.color = gradient.Evaluate(fraction);
    }
}
