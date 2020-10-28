using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HudPrintStringBehaviour : MonoBehaviour
{
    private Text textObj;

    private void Start()
    {
        textObj = GetComponent<Text>();
    }

    public void DisplayString(string text)
    {
        textObj.text = text;
    }
}
