using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Move : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    [SerializeField] float time;

    private void Start()
    {
        transform.DOLocalMove(direction, time).SetEase(Ease.OutQuad);
    }
}
