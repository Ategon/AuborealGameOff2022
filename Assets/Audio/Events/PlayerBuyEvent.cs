using Assets.EventSystem;
using UnityEngine;

namespace Assets.Audio.Events
{
    [CreateAssetMenu(fileName = nameof(PlayerBuyEvent), menuName = "Events/Audio/PlayerBuyEvent")]
    public class PlayerBuyEvent : BaseEvent<EventParameters>
    {

    }
}