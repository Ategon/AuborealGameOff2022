using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    [SerializeField] private float bobAmount;
    [SerializeField] private float bobSpeed;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;
    }

    private void FixedUpdate()
    {
        
        transform.localPosition = new Vector3(transform.localPosition.x, startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobAmount, transform.localPosition.z);
    }
}
