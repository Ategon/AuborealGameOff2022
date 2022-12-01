using System.Collections;
using System.Collections.Generic;
using Assets.Enemies;
using UnityEngine;

public class EnemyAnimationEventListener : MonoBehaviour
{
    [SerializeField] private EnemyMeleeAttack enemyMeleeAttack;
    [SerializeField] private EnemyShootProjectile enemyShootProjectile;
    [SerializeField] private EnemyHealth enemyHealth;

    public void DealAttackDamage()
    {
        enemyMeleeAttack.DealAttackDamage();
    }

    public void FinishAttackAnimation()
    {
        enemyMeleeAttack.FinishAttackAnimation();
    }

    public void CreateProjectile()
    {
        enemyShootProjectile.CreateProjectile();
    }

    public void FinishDying()
    {
        enemyHealth.RemoveFromGame();
    }
}
