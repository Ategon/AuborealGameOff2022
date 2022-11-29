using System.Collections;
using System.Collections.Generic;
using Assets.Player.Health;
using UnityEngine;

namespace Assets.Enemies
{
    public class EnemyHealth : Health
    {
        [SerializeField] private NumAggroedEnemyChangeEvent numAggroedEnemyChangeEvent;
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
            numAggroedEnemyChangeEvent.Raise(this, new NumEnemyChangeEventParameters(-1));
            Destroy(gameObject);
        }
    }
}
