using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIPatrolBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    public List<Transform> patrolPoints;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    private int i;
    private void Update()
    {
        if (agent.pathPending || !(agent.remainingDistance < 0.5f)) return;
        agent.destination = patrolPoints[i].position;
        i = (i + 1) % patrolPoints.Count;
    }
}
