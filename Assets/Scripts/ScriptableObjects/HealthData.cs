using UnityEngine;

[CreateAssetMenu]
public class HealthData : ScriptableObject
{
    public int health;
    public int maxHealth;

    public void Initialize()
    {
        health = maxHealth;
    }

    public void AffectHealth(int amount)
    {
        health += amount;
        if (health < 0)
        {
            health = 0;
        }
        else if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void SetZero()
    {
        health = 0;
    }
}
