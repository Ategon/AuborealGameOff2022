using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    [Header("Projectile Stats")]
    [SerializeField] public float projectileSpeed;
    public int projectileDamage;
    public float projectileKnockbackMagnitude;

    public void Fire(Vector2 projectileSpawnPoint, Vector2 target, int direction = -1)
    {
        GameObject projectileObject = Instantiate(projectilePrefab, projectileSpawnPoint, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        Vector2 directionVector = (target - projectileSpawnPoint).normalized;
        projectile.rigidbody.velocity = directionVector * projectileSpeed;
        projectile.damage = new Damage(projectileDamage, projectileKnockbackMagnitude, directionVector);
        if (direction != -1 & projectile.downSprite)
        {
            SpriteRenderer spriteRenderer = projectileObject.GetComponentInChildren<SpriteRenderer>();
            switch (direction)
            {
                case 0:
                    spriteRenderer.sprite = projectile.downSprite;
                    break;
                case 1:
                    spriteRenderer.sprite = projectile.leftSprite;
                    break;
                case 2:
                    spriteRenderer.sprite = projectile.upSprite;
                    break;
                case 3:
                    spriteRenderer.sprite = projectile.rightSprite;
                    break;
            }
        }
    }
}
