using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLifeBehaviour : MonoBehaviour
{
    public float respawnTime, invincibleTime;
    public IntData health;
    public FloatData spawnDirection;
    public Vector3Data spawnPoint;
    
    public UnityEvent respawnEvent, deathEvent;

    private CharacterController controller;
    private MeshRenderer meshRenderer;
    private PlayerMoveBehaviour mover;
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
        mover = GetComponent<PlayerMoveBehaviour>();
        respawnEvent.Invoke();
        
        meshRenderer.material.shaderKeywords = new[] {"_EMISSION"};
    }
    //
    // private void OnEnable()
    // {
    //     spawnPoint.SetPositionFromValue(transform);
    //     spawnDirection.SetRotationYFromValue(transform);
    // }
    
    // private void Update()
    // {
    //     if (health.value <= 0)
    //     {
    //         StartCoroutine(Die());
    //     }
    // }

    public void Die()
    {
        if (dead) return;
        dead = true;
        SetActive(false);
        deathEvent.Invoke();
    }

    public void Respawn()
    {
        spawnPoint.SetPositionFromValue(transform);
        spawnDirection.SetRotationYFromValue(transform);
        
        dead = false;
        SetActive(true);
        respawnEvent.Invoke();
        StartCoroutine(Invincibility());
    }

    // private IEnumerator Die()
    // {
    //     if (dead) yield break;
    //     dead = true;
    //     SetActive(false);
    //     deathEvent.Invoke();
    //     
    //     yield return new WaitForSeconds(respawnTime);
    //     
    //     spawnPoint.SetPositionFromValue(transform);
    //     spawnDirection.SetRotationYFromValue(transform);
    //     
    //     dead = false;
    //     SetActive(true);
    //     respawnEvent.Invoke();
    //     StartCoroutine(Invincibility());
    // }

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
}
