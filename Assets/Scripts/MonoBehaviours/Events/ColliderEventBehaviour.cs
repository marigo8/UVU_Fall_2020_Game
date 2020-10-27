using UnityEngine;
using UnityEngine.Events;

public class ColliderEventBehaviour : MonoBehaviour
{
    public string filterTag;
    public UnityEvent<Collider> enterEvent;
    public UnityEvent<Collider> stayEvent;
    public UnityEvent<Collider> exitEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (filterTag != "") // If there is no tag, just invoke the event.
        {
            if (!other.CompareTag(filterTag)) return; // if there is a tag but it doesn't match, don't invoke the event.
        }
        enterEvent.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (filterTag != "") // If there is no tag, just invoke the event.
        {
            if (!other.CompareTag(filterTag)) return; // if there is a tag but it doesn't match, don't invoke the event.
        }
        stayEvent.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (filterTag != "") // If there is no tag, just invoke the event.
        {
            if (!other.CompareTag(filterTag)) return; // if there is a tag but it doesn't match, don't invoke the event.
        }
        exitEvent.Invoke(other);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (filterTag != "") // If there is no tag, just invoke the event.
        {
            if (!other.collider.CompareTag(filterTag)) return; // if there is a tag but it doesn't match, don't invoke the event.
        }
        enterEvent.Invoke(other.collider);
    }

    private void OnCollisionStay(Collision other)
    {
        if (filterTag != "") // If there is no tag, just invoke the event.
        {
            if (!other.collider.CompareTag(filterTag)) return; // if there is a tag but it doesn't match, don't invoke the event.
        }
        stayEvent.Invoke(other.collider);
    }

    private void OnCollisionExit(Collision other)
    {
        if (filterTag != "") // If there is no tag, just invoke the event.
        {
            if (!other.collider.CompareTag(filterTag)) return; // if there is a tag but it doesn't match, don't invoke the event.
        }
        exitEvent.Invoke(other.collider);
    }
}
