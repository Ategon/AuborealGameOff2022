using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventListener : MonoBehaviour
{
    [SerializeField] private PlayerMeleeAttack playerMeleeAttack;

    public void DealAttackDamage()
    {
        playerMeleeAttack.DealAttackDamage();
    }

    public void FinishAttackAnimation()
    {
        playerMeleeAttack.FinishAttackAnimation();
    }
}
