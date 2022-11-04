using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionOverTime : MonoBehaviour
{
    private GameTimer gameTimer;
    private ParticleSystem particleSystem;

    [SerializeField] private float nightEmission;
    [SerializeField] private float dayEmission;

    private void Start()
    {
        gameTimer = FindObjectOfType<GameTimer>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        float time = gameTimer.CurrentTime;
        float timeOfDay = time % 1;
        float timeOfDayNormalized = timeOfDay / 0.5f;
        if (timeOfDayNormalized > 1)
        {
            timeOfDayNormalized = 2 - timeOfDayNormalized;
        }

        var emission = particleSystem.emission;
        emission.rateOverTime = Mathf.Lerp(dayEmission, nightEmission, timeOfDayNormalized);
    }
}
