using Assets.EventSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private Transform wavePos;
        [SerializeField] private Transform waveDamagePos;

        [SerializeField] private GameObject bubbleParent;

        private float lowWavePos = -75;
        private float highWavePos = 99.1f;
        [SerializeField] private Color lowWaveColor;
        [SerializeField] private Color highWaveColor;
        [SerializeField] private Color lowDamageColor;
        [SerializeField] private Color highDamageColor;
        [SerializeField] private GameObject bubble;
        private float waveDiff;
        private Image waveRenderer;
        private Image waveDamageRenderer;

        private Tween lastTween = null;
        private Tween lastSecondaryTween = null;
        private Tween lastTertiaryTween = null;
        private Tween lastQuaternaryTween = null;
        private Tween lastQuintenaryTween = null;
        private Tween lastSextenaryTween = null;

        private List<GameObject> bubbles = new List<GameObject>();

        private void Start()
        {
            waveRenderer = wavePos.GetComponent<Image>();
            waveDamageRenderer = waveDamagePos.GetComponent<Image>();
            waveDiff = highWavePos - lowWavePos;
            UpdateMeter();

            for (int i = 0; i < 12; i++)
            {
                GameObject newBubble = Instantiate(bubble, wavePos);
                newBubble.transform.SetParent(bubbleParent.transform);
                newBubble.transform.SetSiblingIndex(0);
                newBubble.transform.localPosition = new Vector3(Random.Range(-60 + i*15, -60 + i*15 + 7.5f), Random.Range(-7.5f, 7.5f), 0);
                bubbles.Add(newBubble);
            }
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

            float start = healthFill.fillAmount;
            float end = (float)healthController.currentHealth / (float)healthController.maxHealth;

            healthText.text = "Health: " + healthController.currentHealth.ToString() + " / " + healthController.maxHealth.ToString();

            if (start > end)
            {
                if (lastTween != null) lastTween.Kill();
                if (lastSecondaryTween != null) lastSecondaryTween.Kill();
                if (lastTertiaryTween != null) lastTertiaryTween.Kill();
                if (lastQuaternaryTween != null) lastQuaternaryTween.Kill();
                if (lastQuintenaryTween != null) lastQuintenaryTween.Kill();
                if (lastSextenaryTween != null) lastSextenaryTween.Kill();

                healthDamageFill.fillAmount = start;
                healthHealedFill.fillAmount = end;
                healthFill.fillAmount = end;
                waveRenderer.color = Color.Lerp(lowWaveColor, highWaveColor, end);
                waveDamageRenderer.color = Color.Lerp(lowDamageColor, highDamageColor, start);

                Color targetColor = Color.Lerp(lowDamageColor, highDamageColor, end);

                wavePos.localPosition = new Vector3(lowWavePos + (waveDiff * end), wavePos.localPosition.y, wavePos.localPosition.z);
                waveDamagePos.localPosition = new Vector3(lowWavePos + (waveDiff * start), waveDamagePos.localPosition.y, waveDamagePos.localPosition.z);
                lastTween = DOTween.To(() => healthDamageFill.fillAmount, x => healthDamageFill.fillAmount = x, end, 1).SetDelay(0.2f).SetEase(Ease.InOutQuad, 0.1f);
                lastTertiaryTween = DOTween.To(() => waveDamageRenderer.color, x => waveDamageRenderer.color = x, targetColor, 1).SetDelay(0.2f).SetEase(Ease.InOutQuad, 0.1f);
                lastSecondaryTween = waveDamagePos.DOLocalMoveX(lowWavePos + (waveDiff * end), 1).SetDelay(0.2f).SetEase(Ease.InOutQuad, 0.1f);
            }
            else if (end > start)
            {
                if (lastQuaternaryTween != null) lastQuaternaryTween.Kill();
                if (lastQuintenaryTween != null) lastQuintenaryTween.Kill();
                if (lastSextenaryTween != null) lastSextenaryTween.Kill();
                
                healthHealedFill.fillAmount = end;
                healthFill.fillAmount = start;
                waveRenderer.color = Color.Lerp(lowWaveColor, highWaveColor, start);

                Color targetColor = Color.Lerp(lowWaveColor, highWaveColor, end);

                wavePos.localPosition = new Vector3(lowWavePos + (waveDiff * start), wavePos.localPosition.y, wavePos.localPosition.z);
                lastQuaternaryTween = DOTween.To(() => healthFill.fillAmount, x => healthFill.fillAmount = x, end, 1).SetDelay(0.2f).SetEase(Ease.InOutQuad, 0.1f);
                lastQuintenaryTween = DOTween.To(() => waveRenderer.color, x => waveRenderer.color = x, targetColor, 1).SetDelay(0.2f).SetEase(Ease.InOutQuad, 0.1f);
                lastSextenaryTween = wavePos.DOLocalMoveX(lowWavePos + (waveDiff * end), 1).SetDelay(0.2f).SetEase(Ease.InOutQuad, 0.1f);
            }
            else
            {
                /*
                if (lastTween != null) lastTween.Kill();
                if (lastSecondaryTween != null) lastSecondaryTween.Kill();
                if (lastTertiaryTween != null) lastTertiaryTween.Kill();
                if (lastQuaternaryTween != null) lastQuaternaryTween.Kill();
                if (lastQuintenaryTween != null) lastQuintenaryTween.Kill();
                if (lastSextenaryTween != null) lastSextenaryTween.Kill();

                waveRenderer.color = Color.Lerp(lowWaveColor, highWaveColor, start);
                waveDamageRenderer.color = Color.Lerp(lowDamageColor, highDamageColor, start);

                healthDamageFill.fillAmount = start;
                healthHealedFill.fillAmount = start;
                healthFill.fillAmount = start;
                wavePos.localPosition = new Vector3(lowWavePos + (waveDiff * start), wavePos.localPosition.y, wavePos.localPosition.z);
                waveDamagePos.localPosition = new Vector3(lowWavePos + (waveDiff * start), waveDamagePos.localPosition.y, waveDamagePos.localPosition.z);
            */
            }
        }
        
        private void OnHealthChange(object sender, EventParameters arg2)
        {
            UpdateMeter();
        }
    }
}
