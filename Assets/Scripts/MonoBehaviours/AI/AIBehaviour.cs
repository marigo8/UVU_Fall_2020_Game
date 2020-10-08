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
    public Transform target;
    private bool canNavigate = true;
    private WaitForFixedUpdate wffu;
    public float holdTime = 1f;
    private WaitForSeconds wfs;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wfs = new WaitForSeconds(holdTime);
    }

    private IEnumerator Navigate()
    {
        canNavigate = true;
        yield return wfs;
        while (canNavigate)
        {
            yield return wffu;
            if (target != null)
            {
                agent.destination = target.position;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("EnemyTarget") && !other.CompareTag("Player")) return;
        target = other.transform;
        canNavigate = false;
        StartCoroutine(Navigate());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("EnemyTarget") && !other.CompareTag("Player")) return;
        if (other.transform != target) return;
        canNavigate = false;
    }
}
