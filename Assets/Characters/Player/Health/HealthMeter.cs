using Assets.EventSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Player.Health
{
    public class HealthMeter : MonoBehaviour
    {
        [SerializeField] private HealthChangedEvent healthChangedEvent;
        [SerializeField] private HealthController healthController;
        [SerializeField] private Image healthFill;
        [SerializeField] private TextMeshProUGUI healthText;
        private void Start()
        {
            UpdateMeter();
        }
        private void OnEnable()
        {
            healthChangedEvent.AddListener(OnHealthChange);
        }
        private void OnDisable()
        {
            healthChangedEvent.RemoveListener(OnHealthChange);
        }

        private void UpdateMeter()
        {
            healthFill.fillAmount = ((float) healthController.currentHealth) / healthController.maxHealth;
            healthText.text = "Health: " + healthController.currentHealth + " / " + healthController.maxHealth;
        }
        private void OnHealthChange(object sender, EventParameters arg2)
        {
            UpdateMeter();
        }
    }
}
