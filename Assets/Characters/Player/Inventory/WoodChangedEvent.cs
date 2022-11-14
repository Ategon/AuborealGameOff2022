using Assets.EventSystem;
using UnityEngine;

namespace Assets.Player.Inventory
{
    [CreateAssetMenu(fileName = nameof(WoodChangedEvent), menuName = "Events/WoodChangedEvent")]
    public class WoodChangedEvent : BaseEvent<EventParameters>
    {

    }
}