using Assets.EventSystem;
using UnityEngine;

namespace Assets.Audio.Events
{
    [CreateAssetMenu(fileName = nameof(PlayerDashEvent), menuName = "Events/Audio/PlayerDashEvent")]
    public class PlayerDashEvent : BaseEvent<EventParameters>
    {

    }
}