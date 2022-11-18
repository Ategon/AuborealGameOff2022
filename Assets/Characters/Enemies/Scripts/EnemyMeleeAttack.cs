using System.Collections;
using System.Collections.Generic;
using Assets.Audio.Events;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeAttack : MeleeAttack
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [Header("Attack Momentum")]
    public float attackVelocity;
    public bool isAttacking;
    public Vector2 attackDirection;
    public bool justAttacked;

    private void Update()
    {
        if (isAttacking)
        {
            navMeshAgent.velocity = attackDirection * attackVelocity;
        }
    }

    public void BeginAttacking(Vector2 target)
    {
        navMeshAgent.ResetPath();
        animator.SetTrigger("Attack");
        attackDirection = (new Vector2(target.x - hitboxCenter.position.x, target.y - hitboxCenter.position.y)).normalized;
        isAttacking = true;
    }
    public void DealAttackDamage()
    {
        Attack(hitboxCenter.position + new Vector3(attackDirection.x, attackDirection.y, 0));
    }

    public void FinishAttackAnimation()
    {
        isAttacking = false;
    }

}
