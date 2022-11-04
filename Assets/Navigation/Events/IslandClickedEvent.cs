using Assets.EventSystem;
using UnityEngine;

namespace Assets.Navigation
{
    [CreateAssetMenu(fileName = nameof(IslandClickedEvent), menuName = "Events/IslandClickedEvent")]
    public class IslandClickedEvent : BaseEvent<EventParameters>
    {

    }
}