using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TriggerEventClass : UnityEvent<Collider> {}

public class TriggerBehaviour : MonoBehaviour
{
    public TriggerEventClass triggerEnterEvent;
    public TriggerEventClass triggerStayEvent;
    public TriggerEventClass triggerExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        triggerEnterEvent.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        triggerStayEvent.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        triggerExitEvent.Invoke(other);
    }
}
