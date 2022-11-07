using Assets.EventSystem;
using UnityEngine;

namespace Assets.Navigation
{
    [CreateAssetMenu(fileName = nameof(IslandMouseExit), menuName = "Events/IslandMouseExit")]
    public class IslandMouseExit : BaseEvent<EventParameters>
    {

    }
}