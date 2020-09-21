using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class FireAmmo : MonoBehaviour
{
    protected abstract UnityEvent MouseDownEvent();
    private void OnMouseDown()
    {
        MouseDownEvent();
    }
}
