using Assets.EventSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace Assets.Player.Thirst
{
    public class ThirstMeter : MonoBehaviour
    {
        [SerializeField] private ThirstChangedEvent thirstChangedEvent;
        [SerializeField] private ThirstController thirstController;
        [SerializeField] private Image thirstFill;
        [SerializeField] private TextMeshProUGUI thirstText;
        [SerializeField] private Transform wavePos;

        private float lowWavePos = -92f;
        private float highWavePos = 98.5f;
        private float waveDiff;
        [SerializeField] private Color lowWaveColor;
        [SerializeField] private Color highWaveColor;

        private Tween lastTween = null;
        private Tween lastSecondaryTween = null;
        private Tween lastTertiaryTween = null;

        private Image waveRenderer;

        private void Start()
        {
            waveRenderer = wavePos.GetComponent<Image>();
            waveDiff = highWavePos - lowWavePos;
            UpdateMeter();
        }
        private void OnEnable()
        {
            thirstChangedEvent.AddListener(OnThirstChange);
        }
        private void OnDisable()
        {
            thirstChangedEvent.RemoveListener(OnThirstChange);
        }

        private void UpdateMeter()
        {
            float start = thirstFill.fillAmount;
            float end = (float)thirstController.currentThirst / (float)thirstController.maxThirst;

            thirstText.text = "Thirst: " + thirstController.currentThirst + " / " + thirstController.maxThirst;
            wavePos.localPosition = new Vector3(lowWavePos + (waveDiff * start), wavePos.localPosition.y, wavePos.localPosition.z);
            waveRenderer.color = Color.Lerp(lowWaveColor, highWaveColor, start);

            if (lastTween != null) lastTween.Pause();
            if (lastSecondaryTween != null) lastSecondaryTween.Pause();
            if (lastTertiaryTween != null) lastTertiaryTween.Pause();

            Color targetColor = Color.Lerp(lowWaveColor, highWaveColor, end);

            lastTween = DOTween.To(() => thirstFill.fillAmount, x => thirstFill.fillAmount = x, end, 1).SetEase(Ease.InOutQuad, 0.1f);
            lastSecondaryTween = wavePos.DOLocalMoveX(lowWavePos + (waveDiff * end), 1).SetEase(Ease.InOutQuad, 0.1f);
            lastTertiaryTween = DOTween.To(() => waveRenderer.color, x => waveRenderer.color = x, targetColor, 1).SetEase(Ease.InOutQuad, 0.1f);
        }
        private void OnThirstChange(object sender, EventParameters arg2)
        {
            UpdateMeter();
        }
    }
}
