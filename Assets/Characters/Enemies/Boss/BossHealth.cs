using System.Collections;
using System.Collections.Generic;
using Assets.Enemies;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    [SerializeField] private GameObject equipmentDropPrefab;
    [SerializeField] private BossHealthChangedEvent bossHealthChangedEvent;

    public override void ChangeHealth(int changeAmount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + changeAmount);
        bossHealthChangedEvent.Raise(this, null);
        if (currentHealth <= 0)
            Die();
    }
    protected override void Die()
    {
        Instantiate(equipmentDropPrefab, transform.position, Quaternion.identity);
        base.Die();
    }
}
