using UnityEngine;

public class LookAtBehaviour : MonoBehaviour
{
    public Vector3Data target;
    public bool lockX, lockY;

    public void LookAtTarget()
    {
        transform.LookAt(target.value);
        
        var eulerRotation = transform.eulerAngles;
        if (lockX)
        {
            eulerRotation.x = 0f;
        }

        if (lockY)
        {
            eulerRotation.y = 0f;
        }
        
        transform.eulerAngles = eulerRotation;
    }
}
