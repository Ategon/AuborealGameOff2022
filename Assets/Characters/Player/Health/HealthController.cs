using Assets.EventSystem;
using Assets.Player.Thirst; 
using UnityEngine;


namespace Assets.Player.Health
{
    [CreateAssetMenu(fileName = nameof(HealthController), menuName = "ScriptableObjects/HealthController")]
    public class HealthController : ScriptableObject
    {
        public int currentHealth;
        public int maxHealth;
        [SerializeField] private int largestMaxHealth;
        [SerializeField] private int smallestMaxHealth;
        [SerializeField] private HealthChangedEvent healthChangedEvent;
        [SerializeField] private ThirstChangedEvent thirstChangedEvent;

        private void OnEnable()
        {
            thirstChangedEvent.AddListener(UpdateMaxHealth);
        }
        private void OnDisable()
        {
            thirstChangedEvent.RemoveListener(UpdateMaxHealth);
        }

        public void ChangeHealth(int changeAmount)
        {
            if (changeAmount < 0)
                currentHealth = Mathf.Max(0, currentHealth + changeAmount);
            if (changeAmount > 0)
                currentHealth = Mathf.Min(maxHealth, currentHealth + changeAmount);
            healthChangedEvent.Raise(this, null);
        }

        public void NewGame()
        {
            currentHealth = maxHealth;
        }

        public void UpdateMaxHealth(object sender, EventParameters arg2)
        {
            ThirstChangedEventParameters eventParameters = arg2 as ThirstChangedEventParameters;
            float thirstProportion = (float) eventParameters.finalThirst / eventParameters.maxThirst;
            maxHealth = smallestMaxHealth + (int)(thirstProportion * (largestMaxHealth - smallestMaxHealth));
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
            healthChangedEvent.Raise(this, null);
        }
    }
}
