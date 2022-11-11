using Assets.EventSystem;
using UnityEngine;

namespace Assets.Player.Inventory
{
    [CreateAssetMenu(fileName = nameof(TreasureChangedEvent), menuName = "Events/TreasureChangedEvent")]
    public class TreasureChangedEvent : BaseEvent<EventParameters>
    {

    }
}