using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[RequireComponent(typeof(NavMeshAgent))]
public class AIBehaviour : MonoBehaviour
{
    public float patrolWaitTime = 1f, patrolSpeed, chaseSpeed;
    public Transform target;
    public List<Transform> patrolPoints = new List<Transform>();
    
    private NavMeshAgent agent;
    private Coroutine aiCoroutine;
    private int currentPatrolPoint;
    
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
        if (target == null || !target.CompareTag("Distraction")) // if there is no target OR the enemy is not distracted
        {
            if (other.CompareTag("Player") || other.CompareTag("Distraction"))
            {
                target = other.transform;
                StartAction(ChaseTarget());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == target)
        {
            target = null;
            StartAction(Patrol());
        }
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
            if (distance < closestDistance)
            {
                closestPoint = i;
                closestDistance = distance;
            }
        }

        currentPatrolPoint = closestPoint;
        
        while (true)
        {
            Debug.Log(currentPatrolPoint);
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


    // public bool isActive = true;
    // public Transform target;
    //
    // public AIActivity actionObj;
    // private Coroutine aiCoroutine;
    //
    // private void Start()
    // {
    //     if (actionObj == null) return;
    //     actionObj.Initialize(GetComponent<NavMeshAgent>(), this);
    //     
    //     StartAction();
    //     
    // }
    //
    // public void SetTarget(Transform newTarget)
    // {
    //     
    // }
    //
    // public void SetAction(AIActivity action)
    // {
    //     actionObj = action;
    // }
    //
    // public void StartAction()
    // {
    //     if (aiCoroutine != null) // if a coroutine is already running...
    //     {
    //         StopCoroutine(aiCoroutine); // stop coroutine
    //     }
    //     aiCoroutine = StartCoroutine(actionObj.Activity()); // start coroutine
    // }


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
