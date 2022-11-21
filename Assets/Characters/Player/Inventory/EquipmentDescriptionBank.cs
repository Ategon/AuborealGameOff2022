
using UnityEngine;

namespace Assets.Player.Inventory
{
    [CreateAssetMenu(fileName = nameof(EquipmentDescriptionBank), menuName = "ScriptableObjects/EquipmentDescriptionBank")]
    public class EquipmentDescriptionBank : ScriptableObject
    {
        public string compassDescription;
        public string diviningRodDescription;

        public string GetEquipmentDescription(string equipmentName)
        {
            switch (equipmentName)
            {
                case "Compass":
                    return compassDescription;
                case "Divining Rod":
                    return diviningRodDescription;
            }
            return "";
        }


    }
}