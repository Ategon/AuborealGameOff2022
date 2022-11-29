using System.Collections;
using System.Collections.Generic;
using Assets.EventSystem;
using Assets.Player.Inventory;
using UnityEngine;

public class EquipmentNotification : MonoBehaviour
{
    [SerializeField] private EquipmentPickedUpEvent equipmentPickedUpEvent;
    [SerializeField] private Dialogue dialogue;

    private void OnEnable()
    {
        equipmentPickedUpEvent.AddListener(OnEquipmentPickup);
    }

    private void OnDisable()
    {
        equipmentPickedUpEvent.RemoveListener(OnEquipmentPickup);
    }
    private void OnEquipmentPickup(object sender, EventParameters arg2)
    {
        dialogue.gameObject.SetActive(true);
        EquipmentPickupEventParameters eventParameters = arg2 as EquipmentPickupEventParameters;
        string[] lines = new string[2];
        lines[0] = "You found the " + eventParameters.equipmentName + "!";
        lines[1] = eventParameters.equipmentDescription;
        dialogue.StartDialogue(lines);
    }
}
