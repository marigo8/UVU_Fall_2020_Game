using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class HudImageArrayBehaviour : MonoBehaviour
{
    public Image imagePrefab;
    public IntData data;

    private RectTransform rectTransform;

    private List<Image> images;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        images = new List<Image>();

        var imageWidth = imagePrefab.rectTransform.sizeDelta.x;
        var offset = rectTransform.position + Vector3.right * imageWidth / 2;
        for (var i = 0; i < data.maxValue; i++)
        {
            var image = Instantiate(imagePrefab, transform);
            images.Add(image);
            
            var imageRect = image.GetComponent<RectTransform>();
            if(imageRect == null) throw new Exception("Didn't work");

            imageRect.position = offset;
            
            offset += Vector3.right * imageWidth;
        }
    }

    public void UpdateHud()
    {
        for (var i = 0; i < data.maxValue; i++)
        {
            images[i].enabled = i < data.value;
        }
    }
}
