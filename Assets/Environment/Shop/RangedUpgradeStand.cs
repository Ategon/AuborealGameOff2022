using UnityEngine;

public class RangedUpgradeStand : ShopStand
{
    [SerializeField] private PlayerShootProjectile PlayerShoot;
    [SerializeField] private int projectileDamage;
    [SerializeField] private int projectileSpeed;
    public override void PurchaseItem()
    {
        PlayerShoot.projectileDamage += projectileDamage;
        PlayerShoot.projectileSpeed += projectileSpeed;
    }

    protected override string GetDescription()
    {
        return "Increases the player's ranged\nattack damage.";
    }

}
