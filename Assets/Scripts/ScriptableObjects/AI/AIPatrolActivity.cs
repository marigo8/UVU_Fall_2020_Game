using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIPatrol", menuName = "AI/AIPatrol")]
public class AIPatrolActivity : AIActivity
{
    public List<Vector3> patrolPoints;
    private readonly WaitForFixedUpdate wffu = new WaitForFixedUpdate();
    private int i;
    
    public override IEnumerator Activity()
    {
        while (IsActive)
        {
            Debug.Log(i);
            if (Agent.pathPending || !(Agent.remainingDistance > 0.5f))
            {
                i = (i + 1) % patrolPoints.Count;
            }
            Agent.destination = patrolPoints[i];

            yield return wffu;
        }


        // while (IsActive)
        // {
        //     if (Agent.pathPending || !(Agent.remainingDistance < 0.5f)) yield break;
        //     Agent.destination = patrolPoints[i];
        //     i = (i + 1) % patrolPoints.Count;
        // }
        // Debug.Log("lol");
    }
}
