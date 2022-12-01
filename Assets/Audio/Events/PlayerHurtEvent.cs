using Assets.EventSystem;
using UnityEngine;

namespace Assets.Audio.Events
{
    [CreateAssetMenu(fileName = nameof(PlayerHurtEvent), menuName = "Events/Audio/PlayerHurtEvent")]
    public class PlayerHurtEvent : BaseEvent<EventParameters>
    {

    }
}