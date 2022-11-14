using Assets.Player.Inventory;
using UnityEngine;

public class TreasurePickup : Interactable
{
    [Header("Treasure Pickup")]
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private int treasureAmount;
    protected override bool Interact()
    {
        inventoryController.ChangeTreasure(treasureAmount);
        return true;
    }
}
