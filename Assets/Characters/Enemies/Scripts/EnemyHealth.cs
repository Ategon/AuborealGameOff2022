using System.Collections;
using System.Collections.Generic;
using Assets.Player.Health;
using UnityEngine;

public class EnemyHealth : Health
{
    public int maxHealth;
    [SerializeField] private int startingHealth;
    [HideInInspector] public int currentHealth;
    private void Awake()
    {
        currentHealth = startingHealth;
    }
    public override void ChangeHealth(int changeAmount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + changeAmount);
        if (currentHealth <= 0)
            Die();
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
