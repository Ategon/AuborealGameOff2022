using Assets.EventSystem;
using UnityEngine;

namespace Assets.Enemies
{
    [CreateAssetMenu(fileName = nameof(BossHealthChangedEvent), menuName = "Events/BossHealthChangedEvent")]
    public class BossHealthChangedEvent : BaseEvent<EventParameters>
    {

    }
}