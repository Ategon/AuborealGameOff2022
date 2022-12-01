using Assets.EventSystem;
using UnityEngine;

namespace Assets.Player.Inventory
{
    [CreateAssetMenu(fileName = nameof(EquipmentPickedUpEvent), menuName = "Events/EquipmentPickedUpEvent")]
    public class EquipmentPickedUpEvent : BaseEvent<EventParameters>
    {

    }
}