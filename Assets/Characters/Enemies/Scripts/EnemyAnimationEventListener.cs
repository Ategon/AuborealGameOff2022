using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEventListener : MonoBehaviour
{
    [SerializeField] private EnemyMeleeAttack enemyMeleeAttack;

    public void DealAttackDamage()
    {
        enemyMeleeAttack.DealAttackDamage();
    }

    public void FinishAttackAnimation()
    {
        enemyMeleeAttack.FinishAttackAnimation();
    }
}
