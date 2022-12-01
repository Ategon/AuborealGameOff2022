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
        if (equipmentType == EquipmentType.Sextant)
        {
            EndBoat endboat = FindObjectOfType<EndBoat>();
            endboat.hasSextant = true;
        }
        return true;
    }
}
