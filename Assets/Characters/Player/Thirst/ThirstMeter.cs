using Assets.EventSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Player.Thirst
{
    public class ThirstMeter : MonoBehaviour
    {
        [SerializeField] private ThirstChangedEvent thirstChangedEvent;
        [SerializeField] private ThirstController thirstController;
        [SerializeField] private Image thirstFill;
        [SerializeField] private TextMeshProUGUI thirstText;
        private void Start()
        {
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
            thirstFill.fillAmount = ((float) thirstController.currentThirst) / thirstController.maxThirst;
            thirstText.text = "Thirst: " + thirstController.currentThirst + " / " + thirstController.maxThirst;
        }
        private void OnThirstChange(object sender, EventParameters arg2)
        {
            UpdateMeter();
        }
    }
}
