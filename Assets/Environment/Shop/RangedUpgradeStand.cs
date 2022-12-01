using Assets.Player.Upgrades;
using UnityEngine;

public class RangedUpgradeStand : ShopStand
{
    [SerializeField] private UpgradeController upgradeController;
    [SerializeField] private int projectileDamage;
    public override void PurchaseItem()
    {
        upgradeController.ChangeRangedDamage(projectileDamage);
    }

    protected override string GetDescription()
    {
        return "Increases the player's ranged\nattack damage by " + projectileDamage + ".";
    }

}
