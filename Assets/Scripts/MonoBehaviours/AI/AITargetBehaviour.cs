using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITargetBehaviour : MonoBehaviour
{
    public int priority;
    public bool activeTarget = true;

    public float life;

    public void SubtractLife()
    {
        life -= Time.fixedDeltaTime;

        if (life <= 0)
        {
            activeTarget = false;
        }
    }
}
