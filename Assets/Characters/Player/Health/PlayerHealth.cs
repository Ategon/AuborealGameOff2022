using System.Collections;
using System.Collections.Generic;
using Assets.Player.Health;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : Health
{
    [SerializeField] private HealthController healthController;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float regenPeriod;
    private float timeToNextHeal;

    private Volume v;
    private Vignette vg;

    private float redTime = 1f;
    private float redTimer;

    private float vgBaseIntensity;

    private void Awake()
    {
        timeToNextHeal = regenPeriod;
    }

    private void Start()
    {
        v = FindObjectOfType<Volume>();
        if(v) v.profile.TryGet(out vg);
        if (vg) vgBaseIntensity = vg.intensity.value;
    }

    void Update()
    {
        if (redTimer > 0)
        {
            redTimer -= Time.deltaTime;
            // change intensity based on sin
            if (vg) vg.intensity.value = vgBaseIntensity + Mathf.Sin(Time.time * 20) / 10;
            if (redTimer <= 0)
            {
                if (vg)
                {
                    vg.intensity.value = vgBaseIntensity;
                    vg.color.value = Color.black;
                }
            }
        }
        timeToNextHeal -= Time.deltaTime;
        if (timeToNextHeal <= 0)
        {
            ChangeHealth(1);
            timeToNextHeal = regenPeriod;
        }

        if (healthController.currentHealth <= 0)
        {
            GetComponentInChildren<PlayerSilhouette>().stopCoroutine = true;
            this.gameObject.SetActive(false);
            gameManager.Lose();
        }
    }
    public override void ChangeHealth(int changeAmount)
    {
        healthController.ChangeHealth(changeAmount);

        if (changeAmount < 0)
        {
            if(vg) vg.color.value = Color.red;
            redTimer = redTime;
        }
    }
}
