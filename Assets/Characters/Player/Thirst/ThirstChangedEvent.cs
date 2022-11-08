using Assets.EventSystem;
using UnityEngine;

namespace Assets.Player.Thirst
{
    [CreateAssetMenu(fileName = nameof(ThirstChangedEvent), menuName = "Events/ThirstChangedEvent")]
    public class ThirstChangedEvent : BaseEvent<EventParameters>
    {

    }
}