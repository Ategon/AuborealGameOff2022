using Assets.EventSystem;
using UnityEngine;

namespace Assets.Audio.Events
{
    [CreateAssetMenu(fileName = nameof(PlayerMeleeAttackEvent), menuName = "Events/PlayerMeleeAttackEvent")]
    public class PlayerMeleeAttackEvent : BaseEvent<EventParameters>
    {
    }
}