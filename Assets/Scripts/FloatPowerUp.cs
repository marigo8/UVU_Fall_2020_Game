using System.Collections;
using UnityEngine;

public class FloatPowerUp : MonoBehaviour
{
    public FloatData playerFloatData, defaultFloatData, newFloatData;
    [SerializeField] private float buffTime = 2f, respawnTime;
    [SerializeField] private bool respawn;

    private void Start()
    {
        playerFloatData.value = defaultFloatData.value;
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterMover>() == null) yield break;
        
        playerFloatData.value += newFloatData.value;
        
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        
        yield return new WaitForSeconds(buffTime);
        
        playerFloatData.value -= newFloatData.value;
        
        if (respawn)
        {
            yield return new WaitForSeconds(respawnTime);
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
