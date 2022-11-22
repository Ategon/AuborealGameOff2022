using Assets.EventSystem;
using Assets.Navigation;
using Assets.Player.Inventory;
using TMPro;
using UnityEngine;
using System;


public class IslandInfoDisplay : MonoBehaviour
{
    [Header("Text Components")]
    [SerializeField] private TextMeshProUGUI basicInfoTextComponent;
    [SerializeField] private TextMeshProUGUI equipmentNameTextComponent;
    [SerializeField] private TextMeshProUGUI equipmentDescriptionTextComponent;
    [SerializeField] private TextMeshProUGUI resourceTextComponent;
    [Header("Mouse Events")]
    [SerializeField] private IslandClickedEvent islandClickedEvent;
    [SerializeField] private IslandMouseEnter islandMouseEnter;
    [SerializeField] private IslandMouseExit islandMouseExit;
    [Header("Inventory")]
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private EquipmentDescriptionBank equipmentDescriptionBank;
    private void OnEnable()
    {
        islandClickedEvent.AddListener(OnIslandClick);
        islandMouseEnter.AddListener(OnIslandEnter);
        islandMouseExit.AddListener(OnIslandExit);
    }
    private void OnDisable()
    {
        islandClickedEvent.RemoveListener(OnIslandClick);
        islandMouseEnter.RemoveListener(OnIslandEnter);
        islandMouseExit.RemoveListener(OnIslandExit);
    }
    public void OnIslandEnter(object sender, EventParameters arg2)
    {
        Island island = sender as Island;
        basicInfoTextComponent.text = "Island name: " + island.islandName + "\n";
        int thirstCost = island.GetThirstCostFromDocked();
        if (thirstCost != 0)
        {
            basicInfoTextComponent.text += "Thirst Cost: " + island.GetThirstCostFromDocked() + "\n";
            basicInfoTextComponent.text += "Click to travel";
        }
        if (inventoryController.compassOwned)
        {
            if (island.equipmentType == EquipmentType.None)
            {
                equipmentNameTextComponent.text = "No equipment present.";
                equipmentDescriptionTextComponent.text = "";
            }
            else
            {
                equipmentNameTextComponent.text = equipmentDescriptionBank.GetEquipmentName(island.equipmentType) + " present!";
                equipmentDescriptionTextComponent.text = equipmentDescriptionBank.GetEquipmentDescription(island.equipmentType);
            }
        }
        resourceTextComponent.text = "";
        if (inventoryController.diviningRodOwned)
        {
            resourceTextComponent.text += "Water: " + island.waterAmount;
        }
        else
        {
            resourceTextComponent.text += "Water: UNKNOWN";
        }
    }
    public void OnIslandExit(object sender, EventParameters arg2)
    {
        ClearText();
    }
    public void OnIslandClick(object sender, EventParameters arg2)
    {
        ClearText();
    }

    private void ClearText()
    {
        basicInfoTextComponent.text = "";
        equipmentNameTextComponent.text = "";
        equipmentDescriptionTextComponent.text = "";
        resourceTextComponent.text = "";
    }

}
