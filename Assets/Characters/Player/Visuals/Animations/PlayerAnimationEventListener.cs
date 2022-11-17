using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventListener : MonoBehaviour
{
    [SerializeField] private PlayerMeleeAttack playerMeleeAttack;
    [SerializeField] private PlayerShootProjectile shootProjectile;

    public void DealAttackDamage()
    {
        playerMeleeAttack.DealAttackDamage();
    }

    public void FinishAttackAnimation()
    {
        playerMeleeAttack.FinishAttackAnimation();
    }

    public void FinishShootAnimation()
    {
        shootProjectile.FinishShootAnimation();
    }

    public void ShootProjectile()
    {
        shootProjectile.ShootProjectile();
    }    
}
