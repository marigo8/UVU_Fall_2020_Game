using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlashBehaviour : MonoBehaviour
{
    public bool startEnabled;

    private Collider col;

    public void Flash(float seconds)
    {
        StartCoroutine(FlashIEnumerator(seconds));
    }
    
    private void Start()
    {
        col = GetComponent<Collider>();
        col.enabled = startEnabled;
    }

    private IEnumerator FlashIEnumerator(float seconds)
    {
        col.enabled = !startEnabled;
        yield return new WaitForSeconds(seconds);
        col.enabled = startEnabled;
    }
}
