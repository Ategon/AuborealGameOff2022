using System.Collections;
using System.Collections.Generic;
using Assets.Player.Thirst;
using UnityEngine;


namespace Assets.Player.Inventory
{
    [CreateAssetMenu(fileName = nameof(InventoryController), menuName = "ScriptableObjects/InventoryController")]
    public class InventoryController : ScriptableObject
    {
        [Header("Wood")]
        public int woodCount;
        [SerializeField] private WoodChangedEvent woodChangedEvent;
        [Header("Treasure")]
        public int treasureCount;
        [SerializeField] private TreasureChangedEvent treasureChangedEvent;
        [Header("Ammo")]
        public int ammoCount;
        [SerializeField] private AmmoChangedEvent ammoChangedEvent;
        [Header("Equipment")]
        [SerializeField] private EquipmentDescriptionBank equipmentDescriptionBank;
        [SerializeField] private EquipmentPickedUpEvent equipmentPickedUpEvent;
        public bool compassOwned;
        public bool diviningRodOwned;

        public bool ChangeWood(int changeAmount)
        {
            if (woodCount + changeAmount >= 0)
            {
                woodCount += changeAmount;
                woodChangedEvent.Raise(this, null);
                return true;
            }
            return false;
        }

        public bool ChangeTreasure(int changeAmount)
        {
            if (treasureCount + changeAmount >= 0)
            {
                treasureCount += changeAmount;
                treasureChangedEvent.Raise(this, null);
                return true;
            }
            return false;
        }

        public bool ChangeAmmo(int changeAmount)
        {
            if (ammoCount + changeAmount >= 0)
            {
                ammoCount += changeAmount;
                ammoChangedEvent.Raise(this, null);
                return true;
            }
            return false;
        }

        public void NewGame()
        {
            treasureCount = 0;
            woodCount = 0;
        }

        public void PickUpCompass()
        {
            EquipmentPickupEventParameters eventParameters = new EquipmentPickupEventParameters("Compass", equipmentDescriptionBank.compassDescription);
            equipmentPickedUpEvent.Raise(this, eventParameters);
            compassOwned = true;
        }

        public void PickUpDiviningRod()
        {
            EquipmentPickupEventParameters eventParameters = new EquipmentPickupEventParameters("Divining Rod", equipmentDescriptionBank.diviningRodDescription);
            equipmentPickedUpEvent.Raise(this, eventParameters);
            diviningRodOwned = true;
        }
    }
    public enum ResourceType
    {
        Wood,
        Treasure
    }
}
