using Assets.Player.Inventory;
using UnityEngine;

public class CompassPickup : Interactable
{
    [Header("Compass")]
    [SerializeField] private InventoryController inventoryController;
    protected override bool Interact()
    {
        inventoryController.PickUpCompass();
        return true;
    }
}
