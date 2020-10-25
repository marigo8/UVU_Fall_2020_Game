using UnityEngine;
using UnityEngine.Events;

public class MouseEventsBehaviour : MonoBehaviour
{
    public UnityEvent mouseDownEvent;
    public UnityEvent mouseDownOnObjectEvent;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownEvent.Invoke();
        }
    }
    private void OnMouseDown()
    {
        mouseDownOnObjectEvent.Invoke();
    }
}
