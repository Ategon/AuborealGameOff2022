using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Player.Health
{
    [CreateAssetMenu(fileName = nameof(HealthController), menuName = "ScriptableObjects/HealthController")]
    public class HealthController : ScriptableObject
    {
        public int currentHealth;
        public int maxHealth;
        [SerializeField] private HealthChangedEvent healthChangedEvent;

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
    }
}
