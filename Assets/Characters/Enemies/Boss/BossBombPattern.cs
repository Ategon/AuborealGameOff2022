using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBombPattern : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private EnemyMovementAnimation enemyMovementAnimation;
    [SerializeField] private Animator animator;
    [SerializeField] private float attackAnimationDuration;
    private float timeUntilAnimationEnd;
    [Header("Sound")]
    [SerializeField] private EnemySound enemySound;
    [Header("Bombs")]
    [SerializeField] private Transform playerHitbox;
    [SerializeField] private float attackCooldown;
    [SerializeField] private SpawnBomb spawnBomb;
    [SerializeField] private float distanceBetweeenBombs;
    private float timeSinceLastAttack;
    [HideInInspector] public bool isAggroed;
    [Header("Circular Pattern")]
    [SerializeField] private float radiusDifference;
    [SerializeField] private float minRadius;
    [SerializeField] private float maxRadius;
    [Header("Ray Pattern")]
    [SerializeField] private float rayAngleDifference;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    void Update()
    {
        if (isAggroed)
        {
            if (timeSinceLastAttack >= attackCooldown)
            {
                Attack();
                timeSinceLastAttack = 0;
            }
            else
            {
                timeSinceLastAttack += Time.deltaTime;
            }
        }
        if (timeUntilAnimationEnd > 0)
        {
            timeUntilAnimationEnd -= Time.deltaTime;
            if (timeUntilAnimationEnd <= 0)
            {
                animator.SetTrigger("FinishAttack");
            }
        }


    }

    private void Attack()
    {
        Invoke("AttackSound", spawnBomb.explosionDelay);
        float random = Random.Range(0, 2);
        enemyMovementAnimation.FaceDirection(playerHitbox.position - transform.position);
        animator.SetTrigger("Attack");
        timeUntilAnimationEnd = attackAnimationDuration;
        if (random == 0)
        {
            CircleAttack();
        }
        else
        {
            RayAttack();
        }
    }
    private void AttackSound()
    {
        enemySound.AttackSound();
    }
    private void CircleAttack()
    {
        float playerRadius = Vector2.Distance(transform.position, playerHitbox.position);
        float angle = Vector2.SignedAngle(transform.position - playerHitbox.position, Vector2.right);
        SpawnBombCircle(angle, playerRadius);
        float iteratingRadius = playerRadius;
        while (iteratingRadius < maxRadius)
        {
            iteratingRadius += radiusDifference;
            SpawnBombCircle(angle, iteratingRadius);
        }
        iteratingRadius = playerRadius;
        while (iteratingRadius > minRadius)
        {
            iteratingRadius -= radiusDifference;
            SpawnBombCircle(angle, iteratingRadius);
        }
    }
    private void SpawnBombCircle(float startAngle, float radius)
    {
        float circumference = radius * 2 * Mathf.PI;
        int numBombs = Mathf.Max((int)(circumference / distanceBetweeenBombs), 1);
        float angleDifference = 360f / numBombs;
        for (float angle = startAngle; angle < startAngle + 360; angle += angleDifference)
        {
            float angleInRadians = angle * Mathf.PI / 180;
            Vector2 bombPosition = new Vector2(transform.position.x - Mathf.Cos(angleInRadians) * radius, transform.position.y + Mathf.Sin(angleInRadians) * radius);
            spawnBomb.CreateBomb(bombPosition);
        }
    }
    private void RayAttack()
    {
        float angle = Vector2.SignedAngle(transform.position - playerHitbox.position, Vector2.right);
        float playerDistance = Vector2.Distance(transform.position, playerHitbox.position);
        for (float iteratingAngle = angle; iteratingAngle < angle + 360; iteratingAngle += rayAngleDifference)
        {
            SpawnRay(iteratingAngle, playerDistance);
        }
    }
    private void SpawnRay(float angle, float startDistance)
    {
        float angleInRadians = angle * Mathf.PI / 180;
        Vector2 rayVector = new Vector2(-Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
        Vector2 bossPosition = new Vector2(transform.position.x, transform.position.y);
        spawnBomb.CreateBomb(bossPosition + rayVector * startDistance);
        float iteratingDistance = startDistance;

        while (iteratingDistance < maxDistance)
        {
            iteratingDistance += distanceBetweeenBombs;
            spawnBomb.CreateBomb(bossPosition + rayVector * iteratingDistance);
        }
        iteratingDistance = startDistance;
        while (iteratingDistance > minDistance)
        {
            iteratingDistance -= distanceBetweeenBombs;
            spawnBomb.CreateBomb(bossPosition + rayVector * iteratingDistance);
        }
    }

}
