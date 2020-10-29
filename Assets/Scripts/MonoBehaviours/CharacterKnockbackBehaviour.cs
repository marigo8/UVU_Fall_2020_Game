using UnityEngine;

public class CharacterKnockbackBehaviour : MonoBehaviour
{
    public float knockbackForce;
    
    public void Knockback(Collider other)
    {
        var character = other.GetComponent<PlayerMoveBehaviour>();
        if (character == null) return;
        
        var direction = other.transform.position - transform.position;
        direction.y = 0;
        direction.Normalize();
        
        character.AddForce(direction * knockbackForce);
    }
}
