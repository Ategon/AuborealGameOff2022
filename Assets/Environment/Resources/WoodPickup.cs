using Assets.Player.Inventory;
using UnityEngine;

public class WoodPickup : Interactable
{
    [Header("Wood Pickup")]
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private int woodAmount;
    protected override bool Interact()
    {
        inventoryController.ChangeWood(woodAmount);
        return true;
    }
}
