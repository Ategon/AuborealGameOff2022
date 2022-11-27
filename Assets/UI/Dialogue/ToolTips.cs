using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ToolTips : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private string line;
    [SerializeField] private float offsize;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        EnterLine();
    }

    public void EnterLine()
    {
        textComponent.text = line;
        Vector2 sizeDelta = GetComponent<RectTransform>().sizeDelta;
        GetComponent<RectTransform>().sizeDelta = new Vector2(offsize * line.Length , sizeDelta.y);
    }

    public void DisableThis()
    {
        this.gameObject.SetActive(false);
    }
}
