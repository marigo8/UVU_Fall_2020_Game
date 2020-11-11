using UnityEngine;

[CreateAssetMenu]
public class Debugger : ScriptableObject
{
    public void OnDebug(string message)
    {
        Debug.Log(message);
    }

    public void DebugCollider(Collider other)
    {
        Debug.Log(other);
    }
}
