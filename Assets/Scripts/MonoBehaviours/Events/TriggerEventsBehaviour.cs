using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerEventsBehaviour : MonoBehaviour
{
    //public string filterTag;
    public IdData filterID;
    
    public UnityEvent<Collider> triggerEnterEvent;
    public UnityEvent<Collider> triggerStayEvent;
    public UnityEvent<Collider> triggerExitEvent;

    public void Destroy(GameObject obj)
    {
        GameObject.Destroy(obj);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (filterID != null)
        {
            if (!filterID.Compare(other.gameObject)) return;
        }
        triggerEnterEvent.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (filterID != null)
        {
            if (!filterID.Compare(other.gameObject)) return;
        }
        triggerStayEvent.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (filterID != null)
        {
            if (!filterID.Compare(other.gameObject)) return;
        }
        triggerExitEvent.Invoke(other);
    }
}