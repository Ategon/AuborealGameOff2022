using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] public float projectileSpeed;
    public int projectileDamage;
    public float projectileKnockbackMagnitude;

    protected void Fire(Vector2 projectileSpawnPoint, Vector2 target)
    {
        GameObject projectileObject = Instantiate(projectilePrefab, projectileSpawnPoint, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        Vector2 direction = (target - projectileSpawnPoint).normalized;
        projectile.rigidbody.velocity = direction * projectileSpeed;
        projectile.damage = new Damage(projectileDamage, projectileKnockbackMagnitude, direction);
    }
}
