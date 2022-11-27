using Assets.EventSystem;
using UnityEngine;

namespace Assets.Audio.Events
{
    [CreateAssetMenu(fileName = nameof(PlayerFootstepEvent), menuName = "Events/Audio/PlayerFootstepEvent")]
    public class PlayerFootstepEvent : BaseEvent<EventParameters>
    {

    }
}