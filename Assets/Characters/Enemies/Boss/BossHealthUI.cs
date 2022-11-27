using Assets.Enemies;
using Assets.EventSystem;
using Assets.Player.Health;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private BossHealthChangedEvent bossHealthChangedEvent;
    [SerializeField] private BossHealth bossHealth;
    [SerializeField] private GameObject healthMeterObject;
    [SerializeField] private Image healthFill;
    [SerializeField] private TextMeshProUGUI healthText;

    private void OnEnable()
    {
        bossHealthChangedEvent.AddListener(OnBossHealthChange);
    }
    private void OnDisable()
    {
        bossHealthChangedEvent.RemoveListener(OnBossHealthChange);
    }
    public void ShowHealth()
    {
        healthMeterObject.SetActive(true);
        bossHealthChangedEvent.Raise(this, null);
    }
    public void HideHealth()
    {
        healthMeterObject.SetActive(false);
    }

    private void OnBossHealthChange(object sender, EventParameters arg2)
    {
        healthFill.fillAmount = ((float)bossHealth.currentHealth) / bossHealth.maxHealth;
        healthText.text = "Boss Health: " + bossHealth.currentHealth + " / " + bossHealth.maxHealth;
    }
}
