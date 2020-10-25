using UnityEngine;
using UnityEngine.Events;

public class MonoEventsBehaviour : MonoBehaviour
{
    public UnityEvent startEvent;
    public UnityEvent enableEvent;
    public UnityEvent updateEvent;
    
    private void Start()
    {
        startEvent.Invoke();
    }

    private void OnEnable()
    {
        enableEvent.Invoke();
    }

    private void Update()
    {
        updateEvent.Invoke();
    }
}
