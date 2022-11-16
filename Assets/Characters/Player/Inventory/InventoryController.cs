using System.Collections;
using System.Collections.Generic;
using Assets.Player.Thirst;
using UnityEngine;


namespace Assets.Player.Inventory
{
    [CreateAssetMenu(fileName = nameof(InventoryController), menuName = "ScriptableObjects/InventoryController")]
    public class InventoryController : ScriptableObject
    {
        public int woodCount;
        [SerializeField] private WoodChangedEvent woodChangedEvent;
        public int treasureCount;
        [SerializeField] private TreasureChangedEvent treasureChangedEvent;
        public int ammoCount;
        [SerializeField] private AmmoChangedEvent ammoChangedEvent;

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
    }
    public enum ResourceType
    {
        Wood,
        Treasure
    }
}
