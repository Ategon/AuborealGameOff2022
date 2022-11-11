using Assets.Player.Inventory;
using UnityEngine;

public class WoodStand : ShopStand
{
    [Header("Wood")]
    [SerializeField] private int woodAmount;
    public override void PurchaseItem()
    {
        inventoryController.ChangeWood(woodAmount);
    }

    protected override string GetDescription()
    {
        return "Gives " + woodAmount + " wood.";
    }
}
