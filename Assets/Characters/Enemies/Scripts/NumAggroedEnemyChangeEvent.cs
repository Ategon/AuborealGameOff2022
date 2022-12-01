using Assets.EventSystem;
using UnityEngine;

namespace Assets.Enemies
{
    [CreateAssetMenu(fileName = nameof(NumAggroedEnemyChangeEvent), menuName = "Events/NumAggroedEnemyChangeEvent")]
    public class NumAggroedEnemyChangeEvent : BaseEvent<EventParameters>
    {

    }
}