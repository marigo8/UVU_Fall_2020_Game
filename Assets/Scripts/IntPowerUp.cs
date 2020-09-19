using System.Collections;
using UnityEngine;

public class IntPowerUp : MonoBehaviour
{
    public IntData playerIntData, defaultIntData, newIntData;
    [SerializeField] private float buffTime = 2f, respawnTime;
    [SerializeField] private bool respawn;

    private void Start()
    {
        playerIntData.value = defaultIntData.value;
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterMover>() == null) yield break;
        
        playerIntData.value = newIntData.value;
        
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        
        yield return new WaitForSeconds(buffTime);
        
        playerIntData.value = defaultIntData.value;
        
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