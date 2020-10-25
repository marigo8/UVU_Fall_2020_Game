using UnityEngine;

public class DistractionBehaviour : MonoBehaviour
{
    public IntData count;
    public float life;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("EnemyPickUp"))
        {
            life -= Time.fixedDeltaTime;

            if (life <= 0)
            {
                Destroy(transform.parent.gameObject);
            }
        }
        else if (other.CompareTag("Player"))
        {
            count.AddToValue(1);
            Destroy(transform.parent.gameObject);
        }
    }
}
