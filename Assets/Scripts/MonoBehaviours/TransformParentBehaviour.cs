using UnityEngine;

public class TransformParentBehaviour : MonoBehaviour
{
    public void SetColliderParent(Collider other)
    {
        other.transform.parent = transform;
    }

    public void ClearColliderParent(Collider other)
    {
        other.transform.parent = null;
    }
}
