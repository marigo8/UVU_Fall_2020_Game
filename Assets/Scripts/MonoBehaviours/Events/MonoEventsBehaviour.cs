using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoEventsBehaviour : MonoBehaviour
{
    public UnityEvent startEvent;
    public UnityEvent enableEvent;
    //public UnityEvent awakeEvent;
    public UnityEvent updateEvent;
    //public UnityEvent fixedUpdateEvent;
    //public UnityEvent destroyEvent;
    
    private void Start()
    {
        startEvent.Invoke();
    }

    private void OnEnable()
    {
        enableEvent.Invoke();
    }

    // private void Awake()
    // {
    //     awakeEvent.Invoke();
    // }

    private void Update()
    {
        updateEvent.Invoke();
    }

    // private void FixedUpdate()
    // {
    //     fixedUpdateEvent.Invoke();
    // }
    //
    // private void OnDestroy()
    // {
    //     destroyEvent.Invoke();
    // }
}
