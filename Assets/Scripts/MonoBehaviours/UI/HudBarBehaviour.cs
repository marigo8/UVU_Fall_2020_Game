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
    public Image background;
    public Gradient backgroundGradient;
    private Image imageObj;
    private bool isBackgroundNull;

    private void Start()
    {
        isBackgroundNull = background == null;
        imageObj = GetComponent<Image>();
    }

    private void Update()
    {
        var fraction = data.GetFraction();

        imageObj.fillAmount = fraction;
        imageObj.color = gradient.Evaluate(fraction);

        if (isBackgroundNull) return;

        background.color = backgroundGradient.Evaluate(fraction);
    }
}
