using Auboreal.Core.DataPipeline;

namespace Auboreal.Core
{

    public interface IHealthData : IData
    {
        float MaxHealth { get; set; }
        float Health { get; set; }

        void IData.Clear()
        {
            MaxHealth = 0;
            Health = 0;
        }
    }

    public interface IReadOnlyHealthData : IReadOnlyData
    {
        float MaxHealth { get; }
        float Health { get; }
    }

    public interface IHealData : IData
    {
        float Heal { get; set; }

        void IData.Clear()
        {
            Heal = 0;
        }
    }

    public interface IReadOnlyHealData : IReadOnlyData
    {
        float Heal { get; }
    }

    public interface IDamageData : IData
    {
        float Damage { get; set; }

        void IData.Clear()
        {
            Damage = 0;
        }
    }

    public interface IReadOnlyDamageData : IReadOnlyData
    {
        float Damage { get; }
    }
}
