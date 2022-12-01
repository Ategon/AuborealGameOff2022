using Assets.EventSystem;
using UnityEngine;

namespace Assets.Player.Upgrades
{
    [CreateAssetMenu(fileName = nameof(MeleeUpgradeEvent), menuName = "Events/MeleeUpgradeEvent")]
    public class MeleeUpgradeEvent : BaseEvent<EventParameters>
    {

    }
}