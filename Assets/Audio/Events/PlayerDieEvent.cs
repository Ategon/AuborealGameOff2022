using Assets.EventSystem;
using UnityEngine;

namespace Assets.Audio.Events
{
    [CreateAssetMenu(fileName = nameof(PlayerDieEvent), menuName = "Events/Audio/PlayerDieEvent")]
    public class PlayerDieEvent : BaseEvent<EventParameters>
    {

    }
}