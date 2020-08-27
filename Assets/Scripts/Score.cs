using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public IntData scoreValue;
    private Text text;
    void Start()
    {
       text = GetComponent<Text>();
    }
    void Update()
    {
        text.text = scoreValue.value.ToString();
    }
}
