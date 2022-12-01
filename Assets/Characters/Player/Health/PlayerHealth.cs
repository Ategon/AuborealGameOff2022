using System.Collections;
using System.Collections.Generic;
using Assets.Player.Health;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using Assets.Audio;
using Assets.Audio.Events;

public class PlayerHealth : Health
{
    [SerializeField] private HealthController healthController;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float regenPeriod;
    [SerializeField] private PlayerHurtEvent playerHurtEvent;
    [SerializeField] private PlayerDieEvent playerDieEvent;
    private float timeToNextHeal;

    private Volume v;
    private Vignette vg;

    private float redTime = 0.7f;
    private float redTimer;

    private float vgBaseIntensity;

    private Tween lastTween;

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
            if (vg) vg.intensity.value = vgBaseIntensity + Mathf.Sin(Time.time * 10) / 20;
            if (redTimer <= 0)
            {
                if (vg)
                {
                    vg.intensity.value = vgBaseIntensity;
                    DOTween.To(() => vg.color.value, x => vg.color.value = x, Color.black, 0.5f);
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
            playerDieEvent.Raise(this, null);
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
            playerHurtEvent.Raise(this, null);
            if(vg) vg.color.value = Color.red;
            redTimer = redTime;
            if (lastTween != null) lastTween.Kill();
        }
    }
}
