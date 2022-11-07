using System;
using UnityEngine;
using Auboreal.Core;
using Auboreal.Core.DataPipeline;
using Auboreal.Core.EventSystem;
using Auboreal.Unity.EventSystem;

namespace Auboreal.Unity
{
    [RequireComponent(typeof(SceneTreeEventNode))]
    public class SimpleHealth : MonoBehaviour, IEventHandler
    {
        [SerializeField]
        private SimpleHealthData data;
        public float MaxHealth { get => data.MaxHealth; }
        public float Health { get => data.Health; }

        public DataBuilder<SimpleHealthData> UpdateHealth { get; private set; }
        public DataBuilder<SimpleDamageData> DamageModifier { get; private set; }
        public DataBuilder<SimpleHealData> HealModifier { get; private set; }

        void Start()
        {
            SceneTreeEventNode eventNode;
            TryGetComponent<SceneTreeEventNode>(out eventNode);
            eventNode.Directory.Handlers.Add(this);
        }

        public SimpleHealth()
        {
            UpdateHealth = new DataBuilder<SimpleHealthData>();
            DamageModifier = new DataBuilder<SimpleDamageData>();
            HealModifier = new DataBuilder<SimpleHealData>();
        }

        public void TakeDamage(SimpleDamageData damage)
        {
            DamageModifier.Build(damage);
            data.Health -= damage.Damage;
            UpdateHealth.Build(data);
        }

        public void Heal(SimpleHealData heal)
        {
            HealModifier.Build(heal);
            data.Health += heal.Heal;
            UpdateHealth.Build(data);
        }

        public void HandleEvent(IEvent e)
        {
        }

        public void HandleEvent(SimpleDamageEvent e)
        {
            TakeDamage(e.damage);
        }
    }

    [Serializable]
    public class SimpleHealthData : IHealthData, IReadOnlySimpleHealthData
    {
        [SerializeField]
        private float maxHealth;
        [SerializeField]
        private float health;

        public float MaxHealth { get => maxHealth; set => maxHealth = value; }
        public float Health { get => health; set => health = value; }
    }

    public interface IReadOnlySimpleHealthData : IReadOnlyHealthData
    {

    }

    [Serializable]
    public class SimpleHealData : IHealData, IReadOnlySimpleHealData
    {
        public float Heal { get; set; }
    }

    public interface IReadOnlySimpleHealData : IReadOnlyHealData
    {

    }

    [Serializable]
    public class SimpleDamageData : IDamageData, IReadOnlySimpleDamageData
    {
        public float Damage { get; set; }
    }

    public interface IReadOnlySimpleDamageData : IReadOnlyDamageData
    {

    }
}