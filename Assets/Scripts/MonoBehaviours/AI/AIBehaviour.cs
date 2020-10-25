using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIBehaviour : MonoBehaviour
{
    public float patrolWaitTime = 1f, patrolSpeed, chaseSpeed;
    public Transform target;
    public List<Transform> patrolPoints = new List<Transform>();
    
    private int currentPatrolPoint;
    private NavMeshAgent agent;
    private Coroutine aiCoroutine;
    
    private readonly WaitForFixedUpdate fixedWait = new WaitForFixedUpdate();
    private WaitForSeconds patrolWait;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolWait = new WaitForSeconds(patrolWaitTime);

        StartAction(Patrol());
    }
    
    

    private void OnTriggerStay(Collider other)
    {
        if (target != null && target.CompareTag("Distraction")) return;
        if (!other.CompareTag("Player") && !other.CompareTag("Distraction")) return;
        
        target = other.transform;
        StartAction(ChaseTarget());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform != target) return;
        
        target = null;
        StartAction(Patrol());
    }

    private void StartAction(IEnumerator coroutine)
    {
        if (aiCoroutine != null) // if a coroutine is already running...
        {
            StopCoroutine(aiCoroutine); // stop coroutine
        }
        aiCoroutine = StartCoroutine(coroutine); // start coroutine
    }

    private IEnumerator Patrol()
    {
        if (patrolPoints.Count <= 0) yield break;
        
        agent.speed = patrolSpeed;
        var closestPoint = 0;
        var closestDistance = Vector3.Distance(transform.position, patrolPoints[closestPoint].position);
        
        for (var i = 1; i < patrolPoints.Count; i++)
        {
            var distance = Vector3.Distance(transform.position, patrolPoints[i].position);
            if (!(distance < closestDistance)) continue;
            
            closestPoint = i;
            closestDistance = distance;
        }

        currentPatrolPoint = closestPoint;
        
        while (true)
        {
            if (agent.pathPending || !(agent.remainingDistance > 0.5f))
            {
                yield return patrolWait;
                agent.destination = patrolPoints[currentPatrolPoint].position;
                currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Count;
            }


            yield return fixedWait;
        }
    }

    private IEnumerator ChaseTarget()
    {
        agent.speed = chaseSpeed;
        while (target != null)
        {
            agent.destination = target.position;
            yield return fixedWait;
        }
        StartAction(Patrol());
    }

    private void OnDrawGizmos()
    {
        for (var i = 0; i < patrolPoints.Count; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(patrolPoints[i].position, 0.5f);
            Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[(i + 1) % patrolPoints.Count].position);
        }
    }
}
