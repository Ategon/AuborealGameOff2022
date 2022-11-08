using UnityEngine;

public class Damage
{
    public int damage;
    public float knockbackMagnitude;
    public Vector2 knockbackDirection;

    public Damage(int damage, float knockbackMagnitude, Vector2 knockbackDirection)
    {
        this.damage = damage;
        this.knockbackMagnitude = knockbackMagnitude;
        this.knockbackDirection = knockbackDirection;
    }   
}
