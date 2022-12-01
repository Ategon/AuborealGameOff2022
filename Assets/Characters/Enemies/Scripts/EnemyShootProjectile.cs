using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyShootProjectile : ShootProjectile
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyMovementAnimation enemyMovementAnimation;
    [SerializeField] private EnemySound enemySound;
    private Vector2 target;

    public void StartShooting(Vector2 target)
    {
        enemyMovementAnimation.FaceDirection(target - new Vector2(transform.position.x, transform.position.y));
        this.target = target;
        animator.SetTrigger("Attack");
    }

    public void CreateProjectile()
    {
        Fire(transform.position, target, enemyMovementAnimation.direction);
        enemySound.AttackSound();
    }

}
