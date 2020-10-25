using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PowerUpBehaviour : MonoBehaviour
{
    public bool respawn;
    public float effectTime, respawnTime;

    public UnityEvent use;
    public UnityEvent reset;

    private MeshRenderer meshRenderer;
    private Collider col;

    private WaitForSeconds effectWait, respawnWait;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
        
        effectWait = new WaitForSeconds(effectTime);
        respawnWait = new WaitForSeconds(respawnTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        
        StartCoroutine(ApplyEffect());
        StartCoroutine(Disable());
    }

    private IEnumerator ApplyEffect()
    {
        use.Invoke();
        yield return effectWait;
        reset.Invoke();
    }

    private IEnumerator Disable()
    {
        if (respawn)
        {
            meshRenderer.enabled = false;
            col.enabled = false;

            yield return respawnWait;

            meshRenderer.enabled = true;
            col.enabled = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
