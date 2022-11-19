using Assets.EventSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using DG.Tweening;

namespace Assets.Player.Health
{
    public class HealthMeter : MonoBehaviour
    {
        [SerializeField] private HealthChangedEvent healthChangedEvent;
        [SerializeField] private HealthController healthController;
        [SerializeField] private Image healthFill;
        [SerializeField] private Image healthDamageFill;
        [SerializeField] private Image healthHealedFill;
        [SerializeField] private TextMeshProUGUI healthText;

        private Tween lastTween = null;
        private Tween lastSecondaryTween = null;

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
            if (lastTween != null)
            {
                lastTween.Pause();
                lastSecondaryTween.Pause();
            }

            float start = healthFill.fillAmount;
            float end = (float)healthController.currentHealth / (float)healthController.maxHealth;

            if (start > end)
            {
                healthDamageFill.fillAmount = start;
                healthHealedFill.fillAmount = end;
                healthFill.fillAmount = end;
                lastSecondaryTween = DOTween.To(() => healthFill.fillAmount, x => healthFill.fillAmount = x, end, 0.15f).SetEase(Ease.InOutQuad, 0.1f);
                lastTween = DOTween.To(() => healthDamageFill.fillAmount, x => healthDamageFill.fillAmount = x, end, 1).SetDelay(0.2f).SetEase(Ease.InOutQuad, 0.1f);
            }
            else
            {
                healthDamageFill.fillAmount = start;
                healthHealedFill.fillAmount = end;
                healthFill.fillAmount = start;
                lastSecondaryTween = DOTween.To(() => healthHealedFill.fillAmount, x => healthHealedFill.fillAmount = x, end, 0.15f).SetEase(Ease.InOutQuad, 0.1f);
                lastTween = DOTween.To(() => healthFill.fillAmount, x => healthFill.fillAmount = x, end, 1).SetDelay(0.2f).SetEase(Ease.InOutQuad, 0.1f);
            }
        }
        
        private void OnHealthChange(object sender, EventParameters arg2)
        {
            UpdateMeter();
        }
    }
}
