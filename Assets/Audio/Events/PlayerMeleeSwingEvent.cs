using Assets.EventSystem;
using UnityEngine;

namespace Assets.Audio.Events
{
    [CreateAssetMenu(fileName = nameof(PlayerMeleeSwingEvent), menuName = "Events/Audio/PlayerMeleeSwingEvent")]
    public class PlayerMeleeSwingEvent : BaseEvent<EventParameters>
    {

    }
}