using Assets.EventSystem;
using UnityEngine;

namespace Assets.Audio.Events
{
    [CreateAssetMenu(fileName = nameof(PlayerShootEvent), menuName = "Events/Audio/PlayerShootEvent")]
    public class PlayerShootEvent : BaseEvent<EventParameters>
    {

    }
}