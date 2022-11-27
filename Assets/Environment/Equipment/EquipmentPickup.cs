using Assets.Player.Inventory;
using UnityEngine;

public class EquipmentPickup : Interactable
{
    [Header("Equipment")]
    [SerializeField] private EquipmentType equipmentType;
    [SerializeField] private InventoryController inventoryController;

    protected override bool Interact()
    {
        inventoryController.AcquireEquipment(equipmentType);
        return true;
    }
}
