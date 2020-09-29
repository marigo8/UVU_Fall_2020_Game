using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[System.Serializable]
public class TriggerEventClass : UnityEvent<Collider> {}

public class ColliderEventBehaviour : MonoBehaviour
{
    public TriggerEventClass enterEvent;
    public TriggerEventClass stayEvent;
    public TriggerEventClass exitEvent;

    private void OnTriggerEnter(Collider other)
    {
        enterEvent.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        stayEvent.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        exitEvent.Invoke(other);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        enterEvent.Invoke(other.collider);
    }

    private void OnCollisionStay(Collision other)
    {
        stayEvent.Invoke(other.collider);
    }

    private void OnCollisionExit(Collision other)
    {
        exitEvent.Invoke(other.collider);
    }
}
