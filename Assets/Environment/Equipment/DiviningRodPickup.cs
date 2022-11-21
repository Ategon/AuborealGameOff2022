using Assets.Player.Inventory;
using UnityEngine;

public class DiviningRodPickup : Interactable
{
    [Header("Divining Rod")]
    [SerializeField] private InventoryController inventoryController;
    protected override bool Interact()
    {
        inventoryController.PickUpDiviningRod();
        return true;
    }
}
