using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;

    private bool followTarget = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            followTarget = !followTarget;
        }
        if (followTarget)
        {
            agent.destination = target.position;
        }
        else
        {
            agent.destination = transform.position;
        }
    }
}
