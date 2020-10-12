using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLifeBehaviour : MonoBehaviour
{
    public UnityEvent respawnEvent, deathEvent;
    
    public float respawnTime, invincibleTime;
    public IntData health;
    public FloatData spawnDirection;
    public Vector3Data spawnPoint;

    private CharacterController controller;
    private MeshRenderer meshRenderer;
    private CharacterMover mover;
    private bool dead, invincible = false;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    public void TakeDamage(int damage)
    {
        if (invincible) return;
        health.AddToValue(-damage);
        StartCoroutine(Invincibility());
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        meshRenderer = GetComponent<MeshRenderer>();
        mover = GetComponent<CharacterMover>();
        respawnEvent.Invoke();
        
        meshRenderer.material.shaderKeywords = new[] {"_EMISSION"};
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
        if (dead) yield break;
        dead = true;
        SetActive(false);
        deathEvent.Invoke();
        
        yield return new WaitForSeconds(respawnTime);
        
        spawnPoint.SetPositionFromValue(transform);
        spawnDirection.SetRotationYFromValue(transform);
        
        dead = false;
        SetActive(true);
        respawnEvent.Invoke();
        StartCoroutine(Invincibility());
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
        meshRenderer.material.SetColor(EmissionColor,Color.red * Mathf.LinearToGammaSpace(10f));
        
        yield return new WaitForSeconds(invincibleTime);
        
        meshRenderer.material.SetColor(EmissionColor,Color.black * Mathf.LinearToGammaSpace(10f));
        invincible = false;
    }

    // private void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     if (hit.gameObject.layer == LayerMask.NameToLayer("Enemy"))
    //     {
    //         TakeDamage(1);
    //         var knockback = hit.gameObject.GetComponent<CharacterKnockbackBehaviour>();
    //         if (knockback != null)
    //         {
    //             knockback.Knockback(controller);
    //         }
    //     }
    // }
}
