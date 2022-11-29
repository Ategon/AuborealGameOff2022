using Assets.EventSystem;

namespace Assets.Enemies
{
    public class NumEnemyChangeEventParameters : EventParameters
    {
        public int changeValue;

        public NumEnemyChangeEventParameters(int changeValue)
        {
            this.changeValue = changeValue;
        }
    }
}
