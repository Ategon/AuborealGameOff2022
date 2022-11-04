using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayLight : MonoBehaviour
{
    [SerializeField] private Color dayColor;
    [SerializeField] private Color nightColor;
    [SerializeField] private float dayIntensity;
    [SerializeField] private float nightIntensity;
    private GameTimer gameTimer;
    private Light2D light;

    void Start()
    {
        gameTimer = GameObject.Find("Game Timer").GetComponent<GameTimer>();
        light = GetComponent<Light2D>();
    }

    void Update()
    {
        float time = gameTimer.CurrentTime;
        float timeOfDay = time % 1;
        float timeOfDayNormalized = timeOfDay / 0.5f;
        if (timeOfDayNormalized > 1)
        {
            timeOfDayNormalized = 2 - timeOfDayNormalized;
        }
        light.color = Color.Lerp(dayColor, nightColor, timeOfDayNormalized);
        light.intensity = Mathf.Lerp(dayIntensity, nightIntensity, timeOfDayNormalized);
    }
}
