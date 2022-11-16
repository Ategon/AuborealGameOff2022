using Assets.EventSystem;

namespace Assets.Player.Thirst
{
    public class ThirstChangedEventParameters : EventParameters
    {
        public int finalThirst;
        public int maxThirst;

        public ThirstChangedEventParameters(int finalThirst, int maxThirst)
        {
            this.finalThirst = finalThirst;
            this.maxThirst = maxThirst;
        }

    }
}