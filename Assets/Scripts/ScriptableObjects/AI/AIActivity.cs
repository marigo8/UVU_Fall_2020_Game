using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIActivity : ScriptableObject
{
    protected NavMeshAgent Agent;
    public bool IsActive = true;
    
    public void Initialize(NavMeshAgent navMeshAgent)
    {
        Agent = navMeshAgent;
    }

    public virtual IEnumerator Activity()
    {
        Debug.Log("Not Implemented");
        yield break;
    }
}
