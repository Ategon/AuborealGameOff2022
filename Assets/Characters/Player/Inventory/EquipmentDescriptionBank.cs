
using UnityEngine;

namespace Assets.Player.Inventory
{
    [CreateAssetMenu(fileName = nameof(EquipmentDescriptionBank), menuName = "ScriptableObjects/EquipmentDescriptionBank")]
    public class EquipmentDescriptionBank : ScriptableObject
    {
        public string compassDescription;

        public string GetEquipmentDescription(string equipmentName)
        {
            switch (equipmentName)
            {
                case "Compass":
                    return compassDescription;
            }
            return "";
        }
    }
}