using Assets.EventSystem;
using UnityEngine;

namespace Assets.Player.Inventory
{
    [CreateAssetMenu(fileName = nameof(AmmoChangedEvent), menuName = "Events/AmmoChangedEvent")]
    public class AmmoChangedEvent : BaseEvent<EventParameters>
    {

    }
}