using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DelayedMoveTo : MonoBehaviour
{
    [SerializeField] private float delayTime;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float time;

    private float timer;
    private bool triggered = false;

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= delayTime && !triggered)
        {
            triggered = true;
            transform.DOLocalMove(transform.localPosition + direction, time).SetEase(Ease.OutQuad);
        }
    }
}
