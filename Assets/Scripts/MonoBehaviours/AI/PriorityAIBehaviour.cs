using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PriorityAIBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    private AITargetBehaviour target;

    public List<AITargetBehaviour> potentialTargets = new List<AITargetBehaviour>();

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void AddPotentialTarget(Collider other)
    {
        var potentialTarget = other.GetComponent<AITargetBehaviour>();
        if (potentialTarget == null) return;
        AddPotentialTarget(potentialTarget);
    }

    private void AddPotentialTarget(AITargetBehaviour potentialTarget)
    {
        if (potentialTargets.Contains(potentialTarget)) return;
        if (!potentialTarget.activeTarget) return;
        
        potentialTargets.Add(potentialTarget);
    }

    public void RemovePotentialTarget(Collider other)
    {
        var potentialTarget = other.GetComponent<AITargetBehaviour>();
        if (potentialTarget == null) return;
        RemovePotentialTarget(potentialTarget);
    }

    private void RemovePotentialTarget(AITargetBehaviour potentialTarget)
    {
        if (!potentialTargets.Contains(potentialTarget)) return;
        
        potentialTargets.Remove(potentialTarget);
    }

    public void SubtractPotentialTargetLife(Collider other)
    {
        var potentialTarget = other.GetComponent<AITargetBehaviour>();
        if (potentialTarget == null) return;
        SubtractPotentialTargetLife(potentialTarget);
    }

    private void SubtractPotentialTargetLife(AITargetBehaviour potentialTarget)
    {
        potentialTarget.SubtractLife();
        if (potentialTarget.life <= 0)
        {
            RemovePotentialTarget(potentialTarget);
        }
    }

    private AITargetBehaviour GetHighestPriorityTarget()
    {
        if (potentialTargets.Count <= 0) return null;
        
        var highestPriorityTarget = potentialTargets[0];

        foreach (var potentialTarget in potentialTargets)
        {
            if(!potentialTarget.activeTarget) continue;
            
            if (potentialTarget.priority > highestPriorityTarget.priority)
            {
                highestPriorityTarget = potentialTarget;
            }
        }

        if (highestPriorityTarget.activeTarget)
        {
            return highestPriorityTarget;
        }
        else
        {
            return null;
        }
    }

    private void Update()
    {
        target = GetHighestPriorityTarget();
        if (target != null)
        {
            agent.destination = target.transform.position;
        }
        else
        {
            potentialTargets.Remove(target);
            agent.destination = transform.position;
        }
    }
}
