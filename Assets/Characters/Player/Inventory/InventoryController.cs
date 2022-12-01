using System;
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
        [SerializeField] private int startingAmmo;
        [SerializeField] private AmmoChangedEvent ammoChangedEvent;
        [Header("Equipment")]
        [SerializeField] private EquipmentDescriptionBank equipmentDescriptionBank;
        [SerializeField] private EquipmentPickedUpEvent equipmentPickedUpEvent;
        public bool compassOwned;
        public bool diviningRodOwned;
        public bool nauticalChartOwned;
        public bool resourceMapOwned;
        public bool sextantOwned;

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
            ammoCount = startingAmmo;
            compassOwned = false;
            diviningRodOwned = false;
            nauticalChartOwned = false;
            resourceMapOwned = false;
            sextantOwned = false;
        }

        public void AcquireEquipment(EquipmentType type)
        {
            string equipmentName = equipmentDescriptionBank.GetEquipmentName(type);
            EquipmentPickupEventParameters eventParameters = new EquipmentPickupEventParameters(equipmentName, equipmentDescriptionBank.GetEquipmentDescription(type));
            equipmentPickedUpEvent.Raise(this, eventParameters);
            switch (type)
            {
                case EquipmentType.Compass:
                    compassOwned = true;
                    break;
                case EquipmentType.DiviningRod:
                    diviningRodOwned = true;
                    break;
                case EquipmentType.NauticalChart:
                    nauticalChartOwned = true;
                    break;
                case EquipmentType.ResourceMap:
                    resourceMapOwned = true;
                    break;
                case EquipmentType.Sextant:
                    sextantOwned = true;
                    break;
            }
        }
    }
    public enum ResourceType
    {
        Wood,
        Treasure
    }

    public enum EquipmentType
    { 
        Compass,
        DiviningRod,
        NauticalChart,
        ResourceMap,
        Sextant,
        None
    }
}
