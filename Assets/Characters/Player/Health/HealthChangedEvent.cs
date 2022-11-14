using Assets.EventSystem;
using UnityEngine;

namespace Assets.Player.Health
{
    [CreateAssetMenu(fileName = nameof(HealthChangedEvent), menuName = "Events/HealthChangedEvent")]
    public class HealthChangedEvent : BaseEvent<EventParameters>
    {

    }
}