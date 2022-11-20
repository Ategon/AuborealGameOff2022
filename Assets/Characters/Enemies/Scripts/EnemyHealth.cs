using System.Collections;
using System.Collections.Generic;
using Assets.Player.Health;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int startingHealth;
    private int currentHealth;
    private void Awake()
    {
        currentHealth = startingHealth;
    }
    public override void ChangeHealth(int changeAmount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + changeAmount);
        if (currentHealth < 0)
            Destroy(gameObject);
    }
}
