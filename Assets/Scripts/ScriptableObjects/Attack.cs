using UnityEngine;

[CreateAssetMenu]
public class Attack : ScriptableObject
{
    public int damage;

    public void AttackPlayerCollider(Collider other)
    {
        var player = other.GetComponent<PlayerLifeBehaviour>();
        if (player == null) return;
        
        player.TakeDamage(damage);
    }
}
