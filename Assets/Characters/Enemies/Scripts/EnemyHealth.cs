using System.Collections;
using System.Collections.Generic;
using Assets.Player.Health;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Enemies
{
    public class EnemyHealth : Health
    {
        [SerializeField] private NumAggroedEnemyChangeEvent numAggroedEnemyChangeEvent;
        [SerializeField] private Animator animator;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private EnemyMovementAnimation enemyMovementAnimation;
        [SerializeField] private EnemyMeleeAttack enemyMeleeAttack;
        [SerializeField] private EnemySound enemySound;
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
            else if (changeAmount < 0 & enemySound)
                enemySound.HurtSound();
        }
        protected virtual void Die()
        {
            numAggroedEnemyChangeEvent.Raise(this, new NumEnemyChangeEventParameters(-1));
            if (enemySound)
                enemySound.DieSound();
            Destroy(enemyMovementAnimation);
            if (agent != null)
            {
                Destroy(agent);
            }
            if (enemyMeleeAttack != null)
            {
                Destroy(enemyMeleeAttack);
            }    
            if (animator != null)
            {
                animator.SetTrigger("Die");
            }
            else
            {
                RemoveFromGame();
            }
        }

        public void RemoveFromGame()
        {
            Destroy(gameObject);
        }
    }
}
