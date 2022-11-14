using Assets.Player.Thirst;
using UnityEngine;

public class WaterRestoreStand : ShopStand
{
    [Header("Thirst Restore")]
    [SerializeField] private ThirstController thirstController;
    [SerializeField] private int thirstAmount;
    public override void PurchaseItem()
    {
        thirstController.ChangeThirst(thirstAmount);
    }

    protected override string GetDescription()
    {
        return "Restores " + thirstAmount + " thirst.\nConsumed immediately.";
    }
}
