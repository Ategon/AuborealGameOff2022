using Assets.Player.Upgrades;
using UnityEngine;

public class MeleeUpgradeStand : ShopStand
{
    [SerializeField] private int meleeDamageAmount;
    [SerializeField] private UpgradeController upgradeController;
    public override void PurchaseItem()
    {
        upgradeController.ChangeMeleeDamage(meleeDamageAmount);
    }

    protected override string GetDescription()
    {
        return "Increases the player's melee\nattack damage by " + meleeDamageAmount + ".";
    }

}
