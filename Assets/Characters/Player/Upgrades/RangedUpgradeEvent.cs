using Assets.EventSystem;
using UnityEngine;

namespace Assets.Player.Upgrades
{
    [CreateAssetMenu(fileName = nameof(RangedUpgradeEvent), menuName = "Events/RangedUpgradeEvent")]
    public class RangedUpgradeEvent : BaseEvent<EventParameters>
    {

    }
}