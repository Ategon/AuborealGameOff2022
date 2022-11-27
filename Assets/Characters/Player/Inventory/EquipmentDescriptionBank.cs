
using System;
using UnityEngine;

namespace Assets.Player.Inventory
{
    [CreateAssetMenu(fileName = nameof(EquipmentDescriptionBank), menuName = "ScriptableObjects/EquipmentDescriptionBank")]
    public class EquipmentDescriptionBank : ScriptableObject
    {
        public string compassDescription;
        public string diviningRodDescription;
        public string nauticalChartDescription;
        public string resourceMapDescription;
        public string sextantDescription;

        public string GetEquipmentDescription(EquipmentType type)
        {
            switch (type)
            {
                case EquipmentType.NauticalChart:
                    return nauticalChartDescription;
                case EquipmentType.Compass:
                    return compassDescription;
                case EquipmentType.DiviningRod:
                    return diviningRodDescription;
                case EquipmentType.ResourceMap:
                    return resourceMapDescription;
                case EquipmentType.Sextant:
                    return sextantDescription;
            }
            return "";
        }

        public string GetEquipmentName(EquipmentType type)
        {
            return System.Text.RegularExpressions.Regex.Replace(Enum.GetName(typeof(EquipmentType), type), "[A-Z]", " $0").Trim();
        }


    }
}