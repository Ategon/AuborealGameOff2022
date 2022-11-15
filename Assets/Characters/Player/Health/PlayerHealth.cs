using System.Collections;
using System.Collections.Generic;
using Assets.Player.Health;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private HealthController healthController;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float regenPeriod;
    private float timeToNextHeal;

    private void Awake()
    {
        timeToNextHeal = regenPeriod;
    }

    void Update()
    {
        timeToNextHeal -= Time.deltaTime;
        if (timeToNextHeal <= 0)
        {
            ChangeHealth(1);
            timeToNextHeal = regenPeriod;
        }

        if (healthController.currentHealth <= 0)
        {
            transform.Find("Visuals").gameObject.SetActive(false);
            gameManager.Win();
        }
    }
    public override void ChangeHealth(int changeAmount)
    {
        healthController.ChangeHealth(changeAmount);
    }
}
