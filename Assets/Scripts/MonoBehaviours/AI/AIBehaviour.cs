using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[RequireComponent(typeof(NavMeshAgent))]
public class AIBehaviour : MonoBehaviour
{
    public AIActivity ai;
    private Coroutine aiCoroutine;

    private void Start()
    {
        if (ai == null) return;
        ai.Initialize(GetComponent<NavMeshAgent>());
        
        if (aiCoroutine != null) // if a coroutine is already running...
        {
            StopCoroutine(aiCoroutine); // stop coroutine
        }
        aiCoroutine = StartCoroutine(ai.Activity()); // start coroutine
    }


    // private NavMeshAgent agent;
    // public Transform player;
    // private Transform target;
    // private bool canNavigate = true, playerInRange = false;
    // private readonly WaitForFixedUpdate wffu = new WaitForFixedUpdate();
    // public float holdTime = 1f;
    // private WaitForSeconds wfs;
    //
    // private void Start()
    // {
    //     agent = GetComponent<NavMeshAgent>();
    //     wfs = new WaitForSeconds(holdTime);
    //     target = player;
    // }
    //
    // private IEnumerator Navigate()
    // {
    //     canNavigate = true;
    //     yield return wfs;
    //     while (canNavigate)
    //     {
    //         yield return wffu;
    //         if (target != null)
    //         {
    //             agent.destination = target.position;
    //         }
    //         else if (playerInRange)
    //         {
    //             target = player;
    //         }
    //     }
    // }
    //
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (!other.CompareTag("EnemyTarget") && !other.CompareTag("Player")) return;
    //     if (other.CompareTag("Player"))
    //     {
    //         playerInRange = true;
    //     }
    //     target = other.transform;
    //     canNavigate = false;
    //     StartCoroutine(Navigate());
    // }
    //
    // private void OnTriggerExit(Collider other)
    // {
    //     if (!other.CompareTag("EnemyTarget") && !other.CompareTag("Player")) return;
    //     if (other.CompareTag("Player"))
    //     {
    //         playerInRange = false;
    //     }
    //     if (other.transform != target) return;
    //     canNavigate = false;
    // }
}
