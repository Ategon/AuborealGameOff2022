using Assets.Player.Inventory;
using UnityEngine;

public class AmmoStand : ShopStand
{
    [Header("Ammo")]
    [SerializeField] private int ammoAmount;
    public override void PurchaseItem()
    {
        inventoryController.ChangeAmmo(ammoAmount);
    }

    protected override string GetDescription()
    {
        return "Gives " + ammoAmount + " ammo.";
    }
}
