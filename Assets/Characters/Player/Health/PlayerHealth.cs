using System.Collections;
using System.Collections.Generic;
using Assets.Player.Health;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private HealthController healthController;
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
    }
    public override void ChangeHealth(int changeAmount)
    {
        healthController.ChangeHealth(changeAmount);
    }
}
