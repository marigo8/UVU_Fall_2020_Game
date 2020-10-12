using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[RequireComponent(typeof(NavMeshAgent))]
public class AIBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;
    private bool canNav = true;
    private WaitForFixedUpdate wffu = new WaitForFixedUpdate();

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        canNav = true;
        while (canNav)
        {
            yield return new WaitForSeconds(2f);
            agent.destination = player.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canNav = false;
    }


    // private NavMeshAgent agent;
    // [FormerlySerializedAs("defaultTarget")] public Transform player;
    // private Transform target;
    // private bool canNavigate = true, playerInRange = false;
    // private WaitForFixedUpdate wffu;
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
