using Assets.Player.Inventory;
using UnityEngine;

public class AmmoPickup : Interactable
{
    [Header("Wood Pickup")]
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private int ammoAmount;
    protected override bool Interact()
    {
        inventoryController.ChangeAmmo(ammoAmount);
        return true;
    }
}
