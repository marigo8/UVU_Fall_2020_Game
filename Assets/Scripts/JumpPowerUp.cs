using System.Collections;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    public IntData playerJumpCount, normalJumpCount, powerUpCount;
    public float waitTime = 2f;

    private void Start()
    {
        playerJumpCount.value = normalJumpCount.value;
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterMover>() == null) yield break;
        
        playerJumpCount.value = powerUpCount.value;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(waitTime);
        playerJumpCount.value = normalJumpCount.value;
        Destroy(gameObject);
    }
}
