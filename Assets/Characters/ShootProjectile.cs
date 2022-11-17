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

    public void Fire(Vector2 projectileSpawnPoint, Vector2 target)
    {
        GameObject projectileObject = Instantiate(projectilePrefab, projectileSpawnPoint, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        Vector2 direction = (target - projectileSpawnPoint).normalized;
        projectile.rigidbody.velocity = direction * projectileSpeed;
        projectile.damage = new Damage(projectileDamage, projectileKnockbackMagnitude, direction);
    }
}
