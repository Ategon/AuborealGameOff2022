using UnityEngine;

public class MeleeUpgradeStand : ShopStand
{
    [SerializeField] private int meleeDamageAmount;
    [SerializeField] private int meleeSizeAmount;
    [SerializeField] private int meleeKnockbackAmount;
    [SerializeField] private MeleeAttack meleeAttack;
    public override void PurchaseItem()
    {
        PlayerMeleeAttack player = GameObject.Find("Player").GetComponent<PlayerMeleeAttack>();
        player.attackDamage +=meleeDamageAmount;
        player.attackSize += meleeSizeAmount;
        player.attackKnockback += meleeKnockbackAmount;

    }

    protected override string GetDescription()
    {
        return "Increases the player's melee\nattack damage.";
    }

}
