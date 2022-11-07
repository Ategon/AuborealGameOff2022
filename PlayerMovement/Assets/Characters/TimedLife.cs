using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedLife : MonoBehaviour
{
    [SerializeField] private float lifeDuration;
    private float timeRemaining;
    private void Awake()
    {
        timeRemaining = lifeDuration;
    }
    private void Update()
    {
        if (timeRemaining < 0)
        {
            Destroy(gameObject);
        }
        timeRemaining -= Time.deltaTime;
    }
}
