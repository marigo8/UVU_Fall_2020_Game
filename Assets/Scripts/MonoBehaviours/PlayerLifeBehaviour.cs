using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLifeBehaviour : MonoBehaviour
{
    public UnityEvent respawnEvent, deathEvent;
    
    [SerializeField] private float respawnTime, invincibleTime;
    [SerializeField] private FloatData health;

    private CharacterController controller;
    private MeshRenderer meshRenderer;
    private CharacterMover mover;
    private bool invincible;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        meshRenderer = GetComponent<MeshRenderer>();
        mover = GetComponent<CharacterMover>();
    }
    private void Update()
    {
        if (health.value <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        deathEvent.Invoke();
        SetActive(false);
        yield return new WaitForSeconds(respawnTime);
        SetActive(true);
        respawnEvent.Invoke();
    }

    private void SetActive(bool isActive)
    {
        controller.enabled = isActive;
        meshRenderer.enabled = isActive;
        mover.enabled = isActive;
        
        
        for (var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(isActive);
        }
    }
    
    private IEnumerator Invincibility()
    {
        invincible = true;
        meshRenderer.material.SetColor("_EmissionColor",Color.red * Mathf.LinearToGammaSpace(10f));
        
        yield return new WaitForSeconds(invincibleTime);
        
        meshRenderer.material.SetColor("_EmissionColor",Color.black * Mathf.LinearToGammaSpace(10f));
        invincible = false;
    }
}
