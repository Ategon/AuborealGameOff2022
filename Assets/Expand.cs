using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Expand : MonoBehaviour
{
    private RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        rect.DOSizeDelta(new Vector2(640, 640), 1.5f).SetEase(Ease.OutQuad);
    }
}
