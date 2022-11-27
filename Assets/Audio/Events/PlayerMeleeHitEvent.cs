using Assets.EventSystem;
using UnityEngine;

namespace Assets.Audio.Events
{
    [CreateAssetMenu(fileName = nameof(PlayerMeleeHitEvent), menuName = "Events/Audio/PlayerMeleeHitEvent")]
    public class PlayerMeleeHitEvent : BaseEvent<EventParameters>
    {

    }
}