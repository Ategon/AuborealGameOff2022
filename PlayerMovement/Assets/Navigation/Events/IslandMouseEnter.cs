using Assets.EventSystem;
using UnityEngine;

namespace Assets.Navigation
{
    [CreateAssetMenu(fileName = nameof(IslandMouseEnter), menuName = "Events/IslandMouseEnter")]
    public class IslandMouseEnter : BaseEvent<EventParameters>
    {

    }
}